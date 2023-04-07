using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Enums;
using Online_Marketplace.Logger.Logger;
using PayStack.Net;

namespace Online_Marketplace.BLL.Implementation.MarketServices
{
    public class PaymentService : IPaymentService
    {
        static IConfiguration _configuration;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderItem> _orderitemRepo;
        private readonly IRepository<Wallet> _walletRepo;
        private readonly IUnitOfWork _unitOfWork;


        public PaymentService(IConfiguration configuration, ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _walletRepo = _unitOfWork.GetRepository<Wallet>();
            _orderRepo = _unitOfWork.GetRepository<Order>();
            _orderitemRepo = _unitOfWork.GetRepository<OrderItem>();


        }

        public async Task<bool> VerifyPaymentAndUpdateOrderStatus(string referenceCode)
        {

            string secret = _configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);

            TransactionVerifyResponse result = payStack.Transactions.Verify(referenceCode);

            if (result.Status)
            {
                var order = await _orderRepo.GetSingleByAsync(o => o.TransactionReference == referenceCode);

                if (order != null)
                {
                    var orderItems = await _orderitemRepo.GetSingleByAsync(
                        oi => oi.OrderId == order.Id,
                        include: oi => oi.Include(oi => oi.Product).ThenInclude(p => p.Seller)
                    );

                    var sellerWallet = await _walletRepo.GetSingleByAsync(w => w.SellerId == orderItems.Product.SellerId);

                    if (sellerWallet != null)
                    {

                        sellerWallet.Balance += order.TotalAmount;


                        order.OrderStatus = OrderStatus.Paid;


                        await _walletRepo.UpdateAsync(sellerWallet);
                        await _orderRepo.UpdateAsync(order);

                        return true;
                    }
                }
            }

            return false;

        }





    }
}
