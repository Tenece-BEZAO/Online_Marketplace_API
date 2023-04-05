using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;
using System.Security.Claims;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.Logger.Logger;

namespace Online_Marketplace.BLL.Implementation.ProfileServices
{
    public class SellerProfileServices : ISellerProfileServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<SellerProfile> _sellerProfileRepo;
        private readonly IRepository<Seller> _sellerRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public SellerProfileServices(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _sellerProfileRepo = _unitOfWork.GetRepository<SellerProfile>();
            _sellerRepo = _unitOfWork.GetRepository<Seller>();
        }

        public async Task<SellerProfile> CreateProfile(SellerProfileDto sellerProfile)
        {
            try
            {
                _logger.LogInfo("Creating Seller user profile");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only sellers are authorized to create buyer profile.");
                }

                var profile = new SellerProfile
                {
                    BusinessName = sellerProfile.BusinessName,
                    BusinessDescription = sellerProfile.BusinessDescription,
                    BusinessCatagories = sellerProfile.BusinessCategories
                };

                Seller seller = await _sellerRepo.GetSingleByAsync(s => s.UserId == userId);

                if (seller == null)
                {
                    throw new Exception("Seller not found");
                }

                profile.SellerIdentity = seller.Id;

                SellerProfile addedProfile = await _sellerProfileRepo.AddAsync(profile);

                return addedProfile;
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

        public async Task<SellerProfile> UpdateProfile(SellerProfileDto sellerProfile)
        {
            try
            {
                _logger.LogInfo("Updating Seller user profile");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only sellers are authorized to update seller profile.");
                }

                Seller seller = await _sellerRepo.GetSingleByAsync(s => s.UserId == userId);

                if (seller == null)
                {
                    throw new Exception("Seller not found");
                }

                SellerProfile profile = await _sellerProfileRepo.GetSingleByAsync(p => p.SellerIdentity == seller.Id);

                if (profile == null)
                {
                    throw new Exception("Seller profile not found");
                }

                profile.BusinessName = sellerProfile.BusinessName;
                profile.BusinessDescription = sellerProfile.BusinessDescription;
                profile.BusinessCatagories = sellerProfile.BusinessCategories;


                await _sellerProfileRepo.UpdateAsync(profile);

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