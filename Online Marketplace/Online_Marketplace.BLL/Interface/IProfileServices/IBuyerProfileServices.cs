using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IProfileServices
{
    public interface IBuyerProfileServices
    {
        Task<BuyerProfile> CreateProfile(BuyerProfileDto buyerProfile);
        void DisplayProfile();
        Task<BuyerProfile> UpdateProfile(BuyerProfileDto buyerProfile);
        void DeleteProfile();
    }
}