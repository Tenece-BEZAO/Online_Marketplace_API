using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.DAL.Entities;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;

namespace Online_Marketplace.Presentation.Controllers
{

    [ApiController]
    [Route("marketplace/[controller]")]
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


                return Ok (product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet ("Search-Products")]
        
        [AllowAnonymous]

        public async Task<ActionResult<List<ProductCreateDto>>> SearchProducts([FromQuery] ProductSearchDto searchDto)
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
        [Authorize(Roles = "Buyer")]
        [HttpPost("add-to-cart")]
       
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            try
            {
                var result = await _productService.AddToCartAsync(productId, quantity);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to add product to cart.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding product with ID {productId} to cart: {ex}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("Edit")]
       
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductCreateDto productDto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProduct(id, productDto);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateProduct)} controller method {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpDelete("Delete/{id}")]
        
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteProduct)} controller method {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller/products")]
        public async Task<ActionResult<List<ProductCreateDto>>> GetSellerProducts()
        {
            try
            {
                var products = await _productService.GetSellerProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving seller products.", error = ex.Message });
            }
        }

      
        [Authorize(Roles = "Buyer")]
        [HttpPost("Add-Reviews")]
        public async Task<IActionResult> AddReview(ReviewDto reviewDto)
        {
            try
            {
                var result = await _productService.AddReview(reviewDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }



}
