using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Implementation.ProfileServices;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.Shared.DTOs;
using System.Data;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/buyers")]
    public class BuyersController : ControllerBase
    {

        private readonly IBuyerServices _buyerServices;
        private readonly IBuyerProfileServices _buyerProfileServices;

        public BuyersController(IBuyerServices buyerServices, IBuyerProfileServices buyerProfileServices)
        {
            _buyerServices = buyerServices;
            _buyerProfileServices = buyerProfileServices;
        }



        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterBuyer([FromBody] BuyerForRegistrationDto buyerForRegistration)
        {

            var response = await _buyerServices.RegisterBuyer(buyerForRegistration);

            return Ok(response);
        }


        [HttpPost("createProfile")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> CreateProfile([FromBody] BuyerProfileDto buyerProfile)
        {
            try
            {
                var response = await _buyerProfileServices.CreateProfile(buyerProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost("updateProfile")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> UpdateProfile([FromBody] BuyerProfileDto buyerProfile)
        {
            try
            {
                var response = await _buyerProfileServices.UpdateProfile(buyerProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
