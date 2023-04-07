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
    public class BuyerProfileServices : IBuyerProfileServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<BuyerProfile> _buyerProfileRepo;
        private readonly IRepository<Buyer> _buyerRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public BuyerProfileServices(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _buyerProfileRepo = _unitOfWork.GetRepository<BuyerProfile>();
            _buyerRepo = _unitOfWork.GetRepository<Buyer>();
        }


        public async Task<BuyerProfile> CreateProfile(BuyerProfileDto buyerProfile)
        {

            _logger.LogInfo("Creating Buyer user profile");

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                _logger.LogError("User is not authorised");
                throw new Exception("Only buyers are authorized to create buyer profile.");
            }

            var profile = new BuyerProfile
            {
                Address = buyerProfile.Address
            };

            Buyer buyer = await _buyerRepo.GetSingleByAsync(s => s.UserId == userId);

            if (buyer == null)
            {
                throw new Exception("Buyer not found");
            }

            profile.BuyerIdentity = buyer.Id;

            BuyerProfile addedProfile = await _buyerProfileRepo.AddAsync(profile);

            return addedProfile;
            
        }


        public void DeleteProfile()
        {
            throw new NotImplementedException();
        }

        public void DisplayProfile()
        {
            throw new NotImplementedException();
        }

        public async Task<BuyerProfile> UpdateProfile(BuyerProfileDto buyerProfile)
        {
            
            _logger.LogInfo("Updating Buyer user profile");

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                _logger.LogError("User is not authorised");
                throw new Exception("Only buyers are authorized to update buyer profile.");
            }

            Buyer buyer = await _buyerRepo.GetSingleByAsync(s => s.UserId == userId);

            if (buyer == null)
            {
                throw new Exception("Buyer not found");
            }

            BuyerProfile profile = await _buyerProfileRepo.GetSingleByAsync(p => p.BuyerIdentity == buyer.Id);

            if (profile == null)
            {
                throw new Exception("Buyer profile not found");
            }

            profile.Address = buyerProfile.Address;

            await _buyerProfileRepo.UpdateAsync(profile);

            return profile;
            
        }
    }
}