using Online_Marketplace.DAL.Enums;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Interface.IMarketServices
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetOrderHistoryAsync();
        public Task<List<OrderDto>> GetSellerOrderHistoryAsync();

        public Task<List<OrderStatusDto>> GetOrderStatusAsync(int orderId);

        public Task<byte[]> GenerateReceiptAsync(int orderId);

        public Task UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto);


        public Task<bool> CheckoutAsync(int cartId, ShippingMethod shippingMethod);

    }
}
