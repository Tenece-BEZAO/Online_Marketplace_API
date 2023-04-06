using Contracts;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Extension;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Implementation.Services
{
    public sealed class SellerServices : ISellerServices
    {

        private readonly IRepository<Seller> _sellerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;

        private readonly IRepository<Wallet> _walletRepo;


        public SellerServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IUserServices userServices)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userServices = userServices;
            _sellerRepo = _unitOfWork.GetRepository<Seller>();
            _walletRepo = _unitOfWork.GetRepository<Wallet>();
        }


        public async Task<string> RegisterSeller(SellerForRegistrationDto sellerForRegistration)
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
                BusinessName = sellerForRegistration.BusinessName,
                UserId = user.Id

            };

            await _sellerRepo.AddAsync(seller);
            await CreateCustomerAccount(seller);

            return $"Registration Successful! You can now start listing your product!";
            

        }

        private async Task CreateCustomerAccount(Seller seller )
        {
             Wallet wallet = new()
            {
                WalletNo = WalletIdGenerator.GenerateWalletId(),
                Balance = 0,
                IsActive = true,
                SellerId = seller.Id,   
            };
            await _walletRepo.AddAsync(wallet);
        }
    }
}
