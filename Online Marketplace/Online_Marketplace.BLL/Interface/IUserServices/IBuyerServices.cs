using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IServices
{
    public interface IBuyerServices
    {
        Task<string> RegisterBuyer(BuyerForRegistrationDto buyerForRegistration);
    }
}
