using Microsoft.AspNetCore.Identity;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

    }

}
