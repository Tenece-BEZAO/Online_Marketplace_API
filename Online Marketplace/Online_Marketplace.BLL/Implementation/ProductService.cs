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
            _buyerRepo = _unitOfWork.GetRepository<Buyer>();

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



      


        public async Task<bool> AddToCartAsync(int productId, int quantity)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var buyer = await _buyerRepo.GetSingleByAsync(b => b.UserId == userId);

                if (buyer == null)
                {
                    throw new Exception("Buyer not found");
                }

                var product = await _productRepo.GetByIdAsync(productId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                if (quantity <= 0)
                {
                    throw new ArgumentException("Invalid quantity.");
                }

                var cart = await _cartRepo.GetSingleByAsync(c => c.BuyerId == buyer.Id, include: q => q.Include(c => c.CartItems));

                if (cart == null)
                {
                    cart = new Cart { BuyerId = buyer.Id };
                    await _cartRepo.AddAsync(cart); 
                   
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

                _logger.LogInfo($"Added product with ID {productId} to cart of buyer with ID {buyer.Id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding product with ID {productId} to cart: {ex}");

                throw;
            }
        }

        public async Task<bool> CheckoutAsync(int cartId)
        {
            try
            {
                var cart = await _cartRepo.GetSingleByAsync(c => c.Id == cartId, include: q => q.Include(c => c.CartItems).ThenInclude(ci => ci.Product));

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

                var order = new Order
                {
                    BuyerId = buyer.Id,
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

                await _orderItemRepo.AddRangeAsync(orderItems);

                await _cartRepo.DeleteAsync(cart);

                _logger.LogInfo($"Checked out cart with ID {cart.Id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while checking out cart with ID {cartId}: {ex}");

                throw;
            }



        }
       
    }
}
