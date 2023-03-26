using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface
{
    public interface ISellerServices
    {
        Task<string> RegisterSeller(SellerForRegistrationDto sellerForRegistration);
    }
}
