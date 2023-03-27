using Contracts;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Implementation
{
    public sealed class SellerServices : ISellerServices
    {

        private readonly IRepository<Seller> _sellerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;


        public SellerServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IUserServices userServices)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userServices = userServices;
            _sellerRepo = _unitOfWork.GetRepository<Seller>();

        }


        public async Task<string> RegisterSeller(SellerForRegistrationDto sellerForRegistration)
        {
            try
            {
                _logger.LogInfo("Creating the Seller as a user first, before assigning the seller role to them and them add them to Sellers table.");

                var user = await _userServices.RegisterUser(new UserForRegistrationDto
                {
                    FirstName = sellerForRegistration.FirstName,
                    LastName = sellerForRegistration.LastName,
                    Email = sellerForRegistration.Email,
                    Password = sellerForRegistration.Password,
                    UserName = sellerForRegistration.UserName
                });

                await _userManager.AddToRoleAsync(user, "Seller");

                var seller = new Seller
                {

                    FirstName = sellerForRegistration.FirstName,
                    LastName = sellerForRegistration.LastName,
                    PhoneNumber = sellerForRegistration.PhoneNumber,
                    Email = sellerForRegistration.Email,
                    BusinessName= sellerForRegistration.BusinessName,
                    UserId = user.Id

                };

                var result = await _sellerRepo.AddAsync(seller);

                return ($"Registration Successful! You can now start listing your product!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(RegisterSeller)} service method {ex}");
                throw;
            }

        }
    }
}
