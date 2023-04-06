using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Enums;
using Online_Marketplace.Logger.Logger;
using PayStack.Net;
using System.Text;

namespace Online_Marketplace.BLL.Implementation.MarketServices
{
    public class PaymentService : IPaymentService
    {
        static IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderItem> _orderitemRepo;
        private readonly IRepository<Wallet> _walletRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Seller> _sellerRepo;



        public PaymentService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _walletRepo = _unitOfWork.GetRepository<Wallet>();
            _orderRepo = _unitOfWork.GetRepository<Order>();
            _orderitemRepo = _unitOfWork.GetRepository<OrderItem>();
            _sellerRepo = _unitOfWork.GetRepository<Seller>();

        }

        public async Task<bool> VerifyPaymentAndUpdateOrderStatus(string referenceCode)
        {
            try
            {
                string secret = _configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
                PayStackApi payStack = new(secret);

                TransactionVerifyResponse result = payStack.Transactions.Verify(referenceCode);

                if (result.Status)
                {

                    var order = await _orderRepo.GetSingleByAsync(o => o.TransactionReference == referenceCode);




                    if (order != null)
                    {




                        order.OrderStatus = OrderStatus.Paid;


                        await _orderRepo.UpdateAsync(order);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while verifying payment:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }

        }

        /*        public async Task<bool> VerifyPaymentAndUpdateOrderStatus(string referenceCode)
        {
            try
            {
                string secret = _configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
                PayStackApi payStack = new(secret);

                TransactionVerifyResponse result = payStack.Transactions.Verify(referenceCode);

                if (result.Status)
                {
                    var order = await _orderRepo.GetSingleByAsync(o => o.TransactionReference == referenceCode);

                    if (order != null)
                    {



                          var orderItems = await _orderitemRepo.GetAllAsync(
                            oi => oi.OrderId ==order.Id,
                            include: oi => oi.Include(oi => oi.Product).ThenInclude(p => p.Seller)
                        );

                        var sellerWallet = await _walletRepo.GetSingleByAsync(w => w.SellerId == orderItems.Pr .SellerId);

                        if (sellerWallet != null)
                        {
                            sellerWallet.Balance += orderItems.Sum(oi => oi.Price * oi.Quantity);
                            order.OrderStatus = OrderStatus.Paid;

                            await _walletRepo.UpdateAsync(sellerWallet);
                            await _orderRepo.UpdateAsync(order);

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while verifying payment:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }
        }
        */




    }
}
