using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;

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
        public async Task<IActionResult> RegisterSeller([FromBody] SellerForRegistrationDto sellerForRegistration)
        {

            var response = await _sellerServices.RegisterSeller(sellerForRegistration);

            return Ok(response);
        }



        [HttpPost("createProfile")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateProfile([FromBody] SellerProfileDto sellerProfile)
        {

            var response = await _sellerProfileServices.CreateProfile(sellerProfile);
            return Ok(response);

        }


        [HttpPost("updateProfile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProfile([FromBody] SellerProfileDto sellerProfile)
        {

            var response = await _sellerProfileServices.UpdateProfile(sellerProfile);
            return Ok(response);


        }
    }
}
