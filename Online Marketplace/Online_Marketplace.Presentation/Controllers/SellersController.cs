using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellersController : ControllerBase
    {

        private readonly ISellerServices _sellerServices;
        private readonly ISellerProfileServices _sellerProfileServices;

        public SellersController(ISellerServices sellerServices, ISellerProfileServices sellerProfileServices)
        {
            _sellerServices = sellerServices;
            _sellerProfileServices = sellerProfileServices;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [SwaggerOperation("Registers a new seller.")]
        [SwaggerResponse(200, "The seller has been successfully registered.", typeof(SellerForRegistrationDto))]
        public async Task<IActionResult> RegisterSeller([FromBody] SellerForRegistrationDto sellerForRegistration)
        {

            var response = await _sellerServices.RegisterSeller(sellerForRegistration);

            return Ok(response);
        }

        [HttpPost("createProfile")]
        [Authorize(Roles = "Seller")]
        [SwaggerOperation("Creates a profile for the seller.")]
        [SwaggerResponse(200, "The seller's profile has been successfully created.", typeof(SellerProfileDto))]
        [SwaggerResponse(500, "An error occurred while creating the seller's profile.")]
        public async Task<IActionResult> CreateProfile([FromBody] SellerProfileDto sellerProfile)
        {
            try
            {
                var response = await _sellerProfileServices.CreateProfile(sellerProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("updateProfile")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation("Updates a seller's profile.")]
        [SwaggerResponse(200, "The seller's profile has been successfully updated.", typeof(SellerProfileDto))]
        [SwaggerResponse(500, "An error occurred while updating the seller's profile.")]
        public async Task<IActionResult> UpdateProfile([FromBody] SellerProfileDto sellerProfile)
        {
            try
            {
                var response = await _sellerProfileServices.UpdateProfile(sellerProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
