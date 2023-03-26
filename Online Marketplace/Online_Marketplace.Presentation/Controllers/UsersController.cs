using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.Shared;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserServices _userServices;


        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }



        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var response = await _userServices.RegisterUser(userForRegistration);

            return Ok(response);
        }
    }
}
