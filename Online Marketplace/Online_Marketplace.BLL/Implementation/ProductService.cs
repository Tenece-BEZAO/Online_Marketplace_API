using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using System.Security.Claims;


namespace Online_Marketplace.BLL.Implementation
{




    public class ProductServices : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Buyer> _buyerRepo;
        private readonly IRepository<Seller> _sellerRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        private User? _user;


        public ProductServices(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {

            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _productRepo = _unitOfWork.GetRepository<Product>();
            _sellerRepo = _unitOfWork.GetRepository<Seller>();
            _cartRepo = _unitOfWork.GetRepository<Cart>();

        }



        public async Task<Product> CreateProduct(ProductCreateDto productDto)
        {
            try
            {


                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("User not found");
                }


                var product = _mapper.Map<Product>(productDto);


                Seller seller = await _sellerRepo.GetSingleByAsync(s => s.UserId == userId);

                if (seller == null)
                {
                    throw new Exception("Seller not found");
                }

                product.SellerId = seller.Id;

                await _productRepo.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(CreateProduct)} service method {ex}");


                throw;
            }
        }


        public async Task<string> UpdateProduct(int productId, ProductCreateDto productDto)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("User not found");
                }

                var existingProduct = await _productRepo.GetSingleByAsync(x => x.Id == productId, include: x => x.Include(x => x.Seller));

                if (existingProduct == null)
                {
                    throw new Exception("Product not found");
                }


                if (existingProduct.Seller.UserId != userId)
                {
                    throw new Exception("You do not have permission to update this product");
                }


                var ProductCreateDto = _mapper.Map(productDto, existingProduct);



                await _productRepo.UpdateAsync(ProductCreateDto);
                await _unitOfWork.SaveChangesAsync();

                return "Product updated successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateProduct)} service method {ex}");
                throw;
            }
        }

        public async Task<string> DeleteProduct(int productId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("User not found");
                }

                var existingProduct = await _productRepo.GetSingleByAsync(x => x.Id == productId);

                if (existingProduct == null)
                {
                    throw new Exception("Product not found");
                }



                if (existingProduct.Seller.UserId != userId)
                {
                    throw new Exception("You do not have permission to delete this product");
                }

                await _productRepo.DeleteAsync(existingProduct);
                await _unitOfWork.SaveChangesAsync();

                return "Product deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteProduct)} service method {ex}");
                throw;
            }
        }






        public async Task<List<ProductCreateDto>> GetSellerProducts()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            Seller seller = await _sellerRepo.GetSingleByAsync(x => x.UserId == userId);

            IEnumerable<Product> sellerPruducts  = await _productRepo.GetByAsync(p => p.SellerId == seller.Id);


              return   _mapper.Map<List<ProductCreateDto>>(sellerPruducts);
           
        }


        public async Task<List<ProductCreateDto>> GetProducts(ProductSearchDto searchDto)
        {
            try
            {

                var products = await _productRepo.GetAllAsync();



                if (!string.IsNullOrEmpty(searchDto.Search))
                {
                    products = products.Where(p => p.Name.Contains(searchDto.Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }


                var productDtos = _mapper.Map<List<ProductCreateDto>>(products);

                return productDtos;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetProducts)} service method {ex}");


                throw;
            }
        }

        public async Task<List<ProductCreateDto>> ViewProducts()
        {
            try
            {
                var products = await _productRepo.GetAllAsync();


                var productDtos = _mapper.Map<List<ProductCreateDto>>(products);

                return productDtos;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(ViewProducts)} service method {ex}");


                throw;
            }
        }

        

     /*   public async Task<Order> CheckoutAsync()
        {


            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _cartRepo.GetSingleByAsync(c => c.UserId == userId, include: q => q.Include(c => c.CartItems));

            if (cart == null || !cart.CartItems.Any())
            {
                throw new Exception("Cannot checkout empty cart.");
            }

            var orderItems = new List<OrderItem>();
            decimal orderTotal = 0;

            foreach (var cartItem in cart.CartItems)
            {
                var product = await _productRepo.GetByIdAsync(cartItem.ProductId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                if (product.StockQuantity < cartItem.Quantity)
                {
                    throw new Exception("Insufficient product quantity.");
                }

                product.StockQuantity -= cartItem.Quantity;

                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = product.Price
                };

                orderTotal += orderItem.Price * orderItem.Quantity;
                orderItems.Add(orderItem);

                await _productRepo.UpdateAsync(product);
            }

            var order = new Order
            {
                UserId = userId,
                OrderItems = orderItems,
                TotalAmount = orderTotal
            };

            _orderRepo.Add(order);
            await _orderRepo.SaveAsync();

            _cartRepo.Remove(cart);
            await _cartRepo.SaveAsync();

            return order;
        }*/

    /*    public async Task<bool> AddToCartAsync(int productId, int quantity)
        {
            try
            {
                var buyerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var product = await _productRepo.GetByIdAsync(productId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                if (quantity <= 0)
                {
                    throw new ArgumentException("Invalid quantity.");
                }

                var cart = await _cartRepo.GetSingleByAsync(c => c.BuyerId == buyerId, include: q => q.Include(c => c.CartItems));

                if (cart == null)
                {
                    var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == buyerId);
                    if (buyer == null)
                    {
                        throw new Exception("Buyer not found");
                    }

                    cart = new Cart { BuyerId = buyer.Id, Buyer = buyer };
                    await _cartRepo.AddAsync(cart);
                    await _cartRepo.SaveAsync();
                }

                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<CartItem>();
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cartItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity
                    };
                    cart.CartItems.Add(cartItem);
                }

                await _cartRepo.UpdateAsync(cart);

                _logger.LogInfo($"Added product with ID {productId} to cart of buyer with ID {buyerId}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding product with ID {productId} to cart: {ex}");

                throw;
            }
        }*/


        public async Task<bool> AddToCartAsync(int productId, int quantity)
        {
            try
            {
                var buyerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var product = await _productRepo.GetSingleByAsync(x => x.Id == productId);

                

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                if (quantity <= 0)
                {
                    throw new ArgumentException("Invalid quantity.");
                }

                var cart = await _cartRepo.GetSingleByAsync(c => c.Buyer.UserId == buyerId, include: q => q.Include(c => c.CartItems) );

                if (cart == null)
                {
                    var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == buyerId);
                    if (buyer == null)
                    {
                        throw new Exception("Buyer not found");
                    }

                    cart = new Cart { BuyerId = buyer.Id, Buyer = buyer };
                    await _cartRepo.AddAsync(cart);
                    await _cartRepo.SaveAsync();
                }

                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<CartItem>();
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cartItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity
                    };
                    cart.CartItems.Add(cartItem);
                }

                await _cartRepo.UpdateAsync(cart);

                _logger.LogInfo($"Added product with ID {productId} to cart of buyer with ID {buyerId}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding product with ID {productId} to cart: {ex}");

                throw;
            }
        }



    }
}
