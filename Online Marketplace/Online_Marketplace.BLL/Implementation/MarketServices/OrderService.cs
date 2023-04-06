using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Online_Marketplace.BLL.Extension;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.DAL.Enums;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using Org.BouncyCastle.Asn1.X509;
using PayStack.Net;
using System.Security.Claims;
using System.Text;

namespace Online_Marketplace.BLL.Implementation.MarketServices
{
    public class OrderService : IOrderService
    {
        static IConfiguration _configuration;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Buyer> _buyerRepo;
        private readonly IRepository<Seller> _sellerRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Shipping > _shippingRepo;
        private readonly IRepository<OrderItem> _orderitemRepo;
        private readonly IRepository<ProductReviews> _productreivewRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public OrderService(IConfiguration configuration, IPaymentService paymentService, IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;

            _shippingRepo = _unitOfWork.GetRepository<Shipping>();
            _cartRepo = _unitOfWork.GetRepository<Cart>();
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
            try
            {
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
       /* public async Task<bool> CheckoutAsync(int cartId)
        {
            try
            {
                var cart = await _cartRepo.GetSingleByAsync(
                    c => c.Id == cartId,
                    include: q => q.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
                );


                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }

                if (cart.CartItems == null || !cart.CartItems.Any())
                {
                    throw new Exception("Cart is empty");
                }

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == userId);

                if (buyer == null)
                {
                    throw new Exception("Buyer not found");
                }

                var orderReference = OrderReferenceGenerator.GenerateOrderReference();

                var order = new Order
                {
                    BuyerId = buyer.Id,
                    Reference = orderReference,
                    OrderDate = DateTime.UtcNow,
                    OrderStatus = OrderStatus.Pending,
                    TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
                };

                await _orderRepo.AddAsync(order);

                var orderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price,
                    OrderId = order.Id
                }).ToList();

                await _orderitemRepo.AddRangeAsync(orderItems);

                await _cartRepo.DeleteAsync(cart);

                _logger.LogInfo($"Checked out cart with ID {cart.Id}");

               
                var paymentRequest = new PaymentRequestDto
                {
                    Amount = order.TotalAmount,
                    Email = buyer.Email,
                    Reference = Guid.NewGuid().ToString(),
                    CallbackUrl = "https://localhost:7258/marketplace/Products/verifypayment"
                };

                var transaction = await MakePayment(paymentRequest);

                
                order.TransactionReference = transaction.Data.Reference;
                order.PaymentGateway = "paystack";
                order.OrderStatus = OrderStatus.PendingPayment;

                await _orderRepo.UpdateAsync(order);

                _logger.LogInfo($"Payment initiated for order with ID {order.Id}");

                return true;
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while checking out :");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }
        }*/


        public async Task<bool> CheckoutAsync(int cartId, string shippingMethod)
        {
            try
            {
                var cart = await _cartRepo.GetSingleByAsync(
                    c => c.Id == cartId,
                    include: q => q.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
                );

                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }

                if (cart.CartItems == null || !cart.CartItems.Any())
                {
                    throw new Exception("Cart is empty");
                }

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == userId);

                if (buyer == null)
                {
                    throw new Exception("Buyer not found");
                }

                var orderReference = OrderReferenceGenerator.GenerateOrderReference();

                var order = new Order
                {
                    BuyerId = buyer.Id,
                    Reference = orderReference,
                    OrderDate = DateTime.UtcNow,
                    OrderStatus = OrderStatus.Pending,
                    TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
                };

                // Calculate shipping cost and estimated delivery date
                var (shippingCost, estimatedDeliveryDate) = await CalculateShippingCostAsync(shippingMethod.ToString());
                order.ShippingCost = shippingCost;
                order.shippingmethod = shippingMethod ;
                order.EstimateDeliveryDate = estimatedDeliveryDate;
              

                // Add shipping cost to total amount
                order.TotalAmount += shippingCost;

                await _orderRepo.AddAsync(order);

                var orderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price,
                    OrderId = order.Id
                }).ToList();

                await _orderitemRepo.AddRangeAsync(orderItems);

                await _cartRepo.DeleteAsync(cart);

                _logger.LogInfo($"Checked out cart with ID {cart.Id}");

                var paymentRequest = new PaymentRequestDto
                {
                    Amount = order.TotalAmount,
                    Email = buyer.Email,
                    Reference = Guid.NewGuid().ToString(),
                    CallbackUrl = "https://localhost:7258/marketplace/Products/verifypayment"
                };

                var transaction = await MakePayment(paymentRequest);

                order.TransactionReference = transaction.Data.Reference;
                order.PaymentGateway = "paystack";
                order.OrderStatus = OrderStatus.PendingPayment;

                await _orderRepo.UpdateAsync(order);

                _logger.LogInfo($"Payment initiated for order with ID {order.Id}");

                return true;
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while checking out :");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }
        }




        public async Task<(decimal shippingCost, DateTime estimatedDeliveryDate)> CalculateShippingCostAsync(string shipmethod)

        {
            var shipping = Enum.Parse<ShippingMethod>(shipmethod);


            var shippingRate = await _shippingRepo.GetSingleByAsync(sr => sr.ShippingMethod == shipping );

            // Calculate the shipping cost based on the shipping rate and other factors
            decimal shippingCost = 0;
            DateTime estimatedDeliveryDate = DateTime.Now;



            if (shippingRate != null)
            {
                shippingCost = shippingRate.Rate;

                if (shipping == ShippingMethod.Express)
                {
                    shippingCost *= 1.5m; // Increase the shipping cost by 50% for express shipping
                    estimatedDeliveryDate = DateTime.Now.AddDays(2); // Set estimated delivery date to two days from now
                }
                else if (shipping == ShippingMethod.NextDay)
                {
                    shippingCost *= 2.0m; // Increase the shipping cost by 100% for next day shipping
                    estimatedDeliveryDate = DateTime.Now.AddDays(1); // Set estimated delivery date to one day from now
                }
                else // Standard shipping
                {
                    estimatedDeliveryDate = DateTime.Now.AddDays(5); // Set estimated delivery date to five days from now
                }
            }

            return (shippingCost, estimatedDeliveryDate);
        }







        public async Task<TransactionInitializeResponse> MakePayment(PaymentRequestDto paymentRequestDto)
        {
            try
            {
                string secret = _configuration.GetSection("ApiSecret").GetSection("SecretKey").Value;

                var paystackApi = new PayStackApi(secret);

                var transactionInitializeRequest = new TransactionInitializeRequest
                {
                    Email = paymentRequestDto.Email,
                    AmountInKobo = (int)(paymentRequestDto.Amount * 100),
                    Reference = paymentRequestDto.Reference,
                    CallbackUrl = paymentRequestDto.CallbackUrl
                };

                var transactionInitializeResponse = paystackApi.Transactions.Initialize(transactionInitializeRequest);


                var authorizationUrl = transactionInitializeResponse.Data.AuthorizationUrl;
                _httpContextAccessor.HttpContext.Response.Redirect(authorizationUrl);

                return transactionInitializeResponse;
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
       /* public async Task<int> CreateShippingRateAsync(ShippingRateCreateDto shippingRateCreateDto)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                throw new Exception("user not found");
            }

            var create_shipping = _mapper.Map<Shipping>(shippingRateCreateDto);

            var seller = _sellerRepo.GetSingleByAsync(s => s.UserId == userId);

            if(seller == null)
            {
                throw new Exception("seller not found");
            }

            create_shipping.SellerId = seller.Id;



            await _shippingRepo.AddAsync(create_shipping);
            return create_shipping.Id;
       

        
        }*/
    }
}
