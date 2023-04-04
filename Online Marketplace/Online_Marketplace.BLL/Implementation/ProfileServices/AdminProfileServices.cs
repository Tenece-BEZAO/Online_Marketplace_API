using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using System.Security.Claims;

namespace Online_Marketplace.BLL.Implementation.ProfileServices
{
    public class AdminProfileServices : IAdminProfileServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<AdminProfile> _adminProfileRepo;
        private readonly IRepository<Admin> _adminRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public AdminProfileServices(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _adminProfileRepo = _unitOfWork.GetRepository<AdminProfile>();
            _adminRepo = _unitOfWork.GetRepository<Admin>();
        }


        public async Task<AdminProfile> CreateProfile(AdminProfileDto adminProfile)
        {
            try
            {
                _logger.LogInfo("Creating Admin user profile");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId ==null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only admins are authorized to create admin profile.");
                }

                var profile = new AdminProfile
                {

                    Address = adminProfile.Address

                };

                Admin admin = await _adminRepo.GetSingleByAsync(s => s.UserId == userId);

                if (admin == null)
                {
                    throw new Exception("Admin not found");
                }

                profile.AdminIdentity = admin.Id;


                AdminProfile AddProfile = await _adminProfileRepo.AddAsync(profile);

                return AddProfile;
            }

            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(CreateProfile)} service method {ex}");

                throw;
            }
        }

        public void DeleteProfile()
        {
            throw new NotImplementedException();
        }

        public void DisplayProfile()
        {
            throw new NotImplementedException();
        }

        public async Task<AdminProfile> UpdateProfile(AdminProfileDto adminProfile)
        {
            try
            {
                _logger.LogInfo("Updating Admin user profile");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only admins are authorized to update admin profile.");
                }

                Admin admin = await _adminRepo.GetSingleByAsync(s => s.UserId == userId);

                if (admin == null)
                {
                    throw new Exception("Admin not found");
                }

                AdminProfile profile = await _adminProfileRepo.GetSingleByAsync(p => p.AdminIdentity == admin.Id);

                if (profile == null)
                {
                    throw new Exception("Admin profile not found");
                }

                profile.Address = adminProfile.Address;

                await _adminProfileRepo.UpdateAsync(profile);

                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateProfile)} service method {ex}");
                throw;
            }
        }

    }
}