using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Shared;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IUserServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

    }

}
