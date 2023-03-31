using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellersController : ControllerBase
    {

        private readonly ISellerServices _sellerServices;


        public SellersController(ISellerServices sellerServices)
        {
            _sellerServices = sellerServices;
        }



        [HttpPost("register")]
        public async Task<IActionResult> RegisterSeller([FromBody] SellerForRegistrationDto sellerForRegistration)
        {

            var response = await _sellerServices.RegisterSeller(sellerForRegistration);

            return Ok(response);

        }



        [HttpPost("createProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] SellerProfileDto sellerProfile)
        {

            return null;

        }
    }
}
