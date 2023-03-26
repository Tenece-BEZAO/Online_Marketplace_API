using Contracts;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Implementation
{
    public sealed class UserServices : IUserServices
    {

        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;



        public UserServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userRepo = _unitOfWork.GetRepository<User>();
        }




        public async Task<User> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            try
            {
                _logger.LogInfo("Checking if user exist, if not create the user.");
                var existingUser = await _userManager.FindByEmailAsync(userForRegistration.Email.Trim().ToLower());
                if (existingUser != null)
                {
                    throw new InvalidOperationException("Email exists!");
                }

                var user = new User
                {
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    IsSeller = false,
                    UserName = userForRegistration.UserName,
                    Email = userForRegistration.Email,
                    PhoneNumber = userForRegistration.PhoneNumber
                };

                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                if (!result.Succeeded)
                {

                    string errMsg = string.Join("\n", result.Errors.Select(x => x.Description));

                    throw new InvalidOperationException($"Failed to create user:\n{errMsg}");
                }

                return (user);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(RegisterUser)} service method {ex}");
                throw;
            }
        }
        public void GetUserProfile()
        {
            throw new NotImplementedException();
        }

        public void UpdateUserProfile()
        {
            throw new NotImplementedException();
        }
    }
}
