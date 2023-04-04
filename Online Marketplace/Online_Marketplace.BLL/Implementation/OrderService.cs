using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Online_Marketplace.BLL.Extension;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Enums;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using PayStack.Net;
using System.Security.Claims;
using System.Text;

namespace Online_Marketplace.BLL.Implementation
{
    public class OrderService : IOrderService
    {
        
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Buyer> _buyerRepo;
        private readonly IRepository<Seller> _sellerRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderItem> _orderitemRepo;
        private readonly IRepository<ProductReviews> _productreivewRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public OrderService(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {

            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _productRepo = _unitOfWork.GetRepository<Product>();
            _sellerRepo = _unitOfWork.GetRepository<Seller>();
            _buyerRepo = _unitOfWork.GetRepository<Buyer>();
            _orderRepo = _unitOfWork.GetRepository<Order>();
            _orderitemRepo = _unitOfWork.GetRepository<OrderItem>();
            _productreivewRepo = unitOfWork.GetRepository<ProductReviews>();
        }



        public async Task<List<OrderDto>> GetOrderHistoryAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == userId);

                var orders = await _orderRepo.GetAllAsync(o => o.BuyerId == buyer.Id,
                    include: o => o.Include(o => o.OrderItems).ThenInclude(oi => oi.Product));

                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                return orderDtos;
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while getting order history:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }

        }

        public async Task<List<OrderDto>> GetSellerOrderHistoryAsync()
        {
            try {
                var sellerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var seller = await _sellerRepo.GetSingleByAsync(b => b.UserId == sellerId);


                var sellerProducts = await _productRepo.GetAllAsync(p => p.SellerId == seller.Id);



                var productIds = sellerProducts.Select(p => p.Id);

                var orderItems = await _orderitemRepo.GetAllAsync(
               oi => productIds.Contains(oi.ProductId),
               include: oi => oi.Include(oi => oi.Order).ThenInclude(o => o.Buyer)
           );



                var orders = orderItems.GroupBy(oi => oi.OrderId).Select(group =>
                {
                    var order = group.First().Order;

                    var orderDto = _mapper.Map<OrderDto>(order);
                    orderDto.Total = group.Sum(oi => oi.Price * oi.Quantity);
                    orderDto.OrderItems = _mapper.Map<List<OrderItemDto>>(group);

                    return orderDto;
                });

                return orders.ToList();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while getting order history:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }

        }
      



        public async Task<List<OrderStatusDto>> GetOrderStatusAsync(int orderId)
        {

            try
            {
                var buyerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == buyerId);

                var order = await _orderRepo.GetSingleByAsync(o => o.Id == orderId && o.BuyerId == buyer.Id,
                    include: o => o.Include(oi => oi.OrderItems).ThenInclude(oi => oi.Product));

                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                var orderStatuses = order.OrderItems.Select(oi => new OrderStatusDto
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    Status = oi.Order.OrderStatus.ToString(),

                }).ToList();

                return orderStatuses;

            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while getting order history:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }

        }



        public async Task<byte[]> GenerateReceiptAsync(int orderId)
        {

            var sellerid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var seller = await _sellerRepo.GetSingleByAsync(b => b.UserId == sellerid);


            if (seller == null)
            {
                throw new Exception("Seller not found");
            }

            var order = await _orderRepo.GetSingleByAsync(o => o.Id == orderId && o.OrderItems.Any(oi => oi.Product.SellerId == seller.Id),
                include: o => o.Include(oi => oi.OrderItems).ThenInclude(oi => oi.Product).Include(o => o.Buyer));

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            var receipt = new ReceiptDto
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                BuyerName = order.Buyer.FirstName,
                BuyerEmail = order.Buyer.Email,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(oi => new ReceiptItemDto
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            var receiptGenerator = new ReceiptGenerator();
            byte[] receiptBytes;
            using (var receiptStream = receiptGenerator.GenerateReceipt(receipt))
            {
                
                using (var memoryStream = new MemoryStream())
                {
                    await receiptStream.CopyToAsync(memoryStream);
                    receiptBytes = memoryStream.ToArray();
                }
            }

            return receiptBytes;
        }



        public async Task UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto)
        {
            try
            {
                var sellerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var seller = await _sellerRepo.GetSingleByAsync(s => s.UserId == sellerId);

                var order = await _orderRepo.GetSingleByAsync(o => o.Id == updateOrderStatusDto.OrderId && o.OrderItems.Any(oi => oi.Product.SellerId == seller.Id && oi.Product.Id == updateOrderStatusDto.ProductId));

                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                var newStatus = Enum.Parse<OrderStatus>(updateOrderStatusDto.Status);
                order.OrderStatus = newStatus;

                await _orderRepo.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while updating order status:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }
        }

       

      /*  public TransactionInitializeResponse MakePayment(PaymentDto paymentRequestDto)
        {
            string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionInitializeRequest initializeRequest = _mapper.Map<TransactionInitializeRequest>(paymentRequestDto);
            var result = payStack.Transactions.Initialize(initializeRequest);
            return result.Data.AuthorizationUrl;
        }



        public TransactionVerifyResponse VerifyPayment(string referenceCode)
        {
            string secret = (string)_configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;
            PayStackApi payStack = new(secret);
            TransactionVerifyResponse result = payStack.Transactions.Verify(referenceCode);
            return result;

        }*/
    }
}
