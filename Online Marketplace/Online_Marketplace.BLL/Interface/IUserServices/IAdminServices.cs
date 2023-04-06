using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IServices
{
    public interface IAdminServices
    {
        Task<string> RegisterAdmin(AdminForRegistrationDto adminForRegistration);
    }
}
