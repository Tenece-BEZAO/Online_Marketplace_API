using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.Presentation.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILoggerManager _logger;

        public ProductsController(IProductService productService, ILoggerManager logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [Authorize(Roles = "Seller")]
        [HttpPost("Create-Products")]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productdto)
        {
            try
            {
                var product = await _productService.CreateProduct(productdto);

                return Ok (CreatedAtRoute("GetProductById", new { id = product.Id }, product));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet ("Search-Products")]

        [AllowAnonymous]
    
        public async Task<ActionResult<List<ProductCreateDto>>> GetProducts([FromQuery] ProductSearchDto searchDto)
        {
            try
            {
                var products = await _productService.GetProducts(searchDto);

                return Ok(products);
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Something went wrong in the {nameof(GetProducts)} controller action {ex}");

               
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet("All-Products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCreateDto>>> GetProducts()
        {
            try
            {
                var products = await _productService.ViewProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
               
                _logger.LogError($"Something went wrong in the {nameof(GetProducts)} controller action {ex}");

               
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

    }



}
