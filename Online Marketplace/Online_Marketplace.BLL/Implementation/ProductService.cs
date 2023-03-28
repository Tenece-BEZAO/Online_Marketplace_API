using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IRepository<Seller> _sellerRepo;
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









    }
}
