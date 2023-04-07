namespace Online_Marketplace.BLL.Interface.IMarketServices
{
    public interface IPaymentService
    {
        public Task<bool> VerifyPaymentAndUpdateOrderStatus(string referenceCode);

    }
}
