using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface IAdminServices
    {
        Task<string> RegisterAdmin(AdminForRegistrationDto adminForRegistration);
    }
}
