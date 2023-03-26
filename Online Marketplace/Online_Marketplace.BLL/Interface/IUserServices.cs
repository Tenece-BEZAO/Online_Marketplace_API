using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Shared;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface IUserServices
    {
        Task<User> RegisterUser(UserForRegistrationDto userForRegistration);
        void GetUserProfile();
        void UpdateUserProfile();
        
    }
}
