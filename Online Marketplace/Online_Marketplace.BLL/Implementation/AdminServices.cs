using Contracts;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Implementation
{
    public class AdminServices : IAdminServices
    {
        private readonly IRepository<Admin> _adminRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;



        public AdminServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IUserServices userServices)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userServices = userServices;
            _adminRepo = _unitOfWork.GetRepository<Admin>();

        }


        public async Task<string> RegisterAdmin(AdminForRegistrationDto adminForRegistration)
        {
            try
            {
                _logger.LogInfo("Creating the Admin as a user first, before assigning the admin role to them and them add them to Admins table.");

                var user = await _userServices.RegisterUser(new UserForRegistrationDto
                {
                    FirstName = adminForRegistration.FirstName,
                    LastName = adminForRegistration.LastName,
                    Email = adminForRegistration.Email,
                    Password = adminForRegistration.Password,
                    UserName = adminForRegistration.UserName
                });

                await _userManager.AddToRoleAsync(user, "Admin");

                var admin = new Admin
                {

                    FirstName = adminForRegistration.FirstName,
                    LastName = adminForRegistration.LastName,
                    UserName = adminForRegistration.UserName,
                    PhoneNumber = adminForRegistration.PhoneNumber,
                    Email = adminForRegistration.Email,
                    UserId = user.Id

                };

                var result = await _adminRepo.AddAsync(admin);

                return ($"Registration Successful! You now have access as an administrator!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(RegisterAdmin)} service method {ex}");

                throw;
            }

        }
    }
}
