using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IProfileServices
{
    public interface IAdminProfileServices
    {
        Task<AdminProfile> CreateProfile(AdminProfileDto adminProfile);
        void DisplayProfile();
        Task<AdminProfile> UpdateProfile(AdminProfileDto adminProfile);
        void DeleteProfile();
    }
}