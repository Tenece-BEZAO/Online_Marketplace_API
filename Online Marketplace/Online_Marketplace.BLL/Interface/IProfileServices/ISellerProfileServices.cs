using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IProfileServices
{
    public interface ISellerProfileServices
    {
        Task<SellerProfile> CreateProfile(SellerProfileDto sellerProfile);
        void DisplayProfile();
        Task<SellerProfile> UpdateProfile(SellerProfileDto sellerProfile);
        void DeleteProfile();
    }
}