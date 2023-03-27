using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;

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
                var product = _mapper.Map<Product>(productDto);

              
                var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
               

                if (currentUser == null)
                {
                    throw new Exception("User not found");
                }

              
                var seller = await _sellerRepo.GetSingleByAsync(s => s.UserId == currentUser.Id);
                

            
                product.Seller = seller;

                await _productRepo.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError($"Something went wrong in the {nameof(CreateProduct)} service method {ex}");

                // Rethrow the exception
                throw;
            }
        }


    }
}
