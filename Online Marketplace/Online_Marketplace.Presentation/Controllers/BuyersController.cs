using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/buyers")]
    public class BuyersController : ControllerBase
    {

        private readonly IBuyerServices _buyerServices;


        public BuyersController(IBuyerServices buyerServices)
        {
            _buyerServices = buyerServices;
        }



        [HttpPost("register")]
        public async Task<IActionResult> RegisterBuyer([FromBody] BuyerForRegistrationDto buyerForRegistration)
        {

            var response = await _buyerServices.RegisterBuyer(buyerForRegistration);

            return Ok(response);

        }
    }
}
