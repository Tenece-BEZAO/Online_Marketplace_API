using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;

namespace Online_Marketplace.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authentication;
        public AuthenticationController(IAuthService authentication) => _authentication = authentication;


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _authentication.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authentication.ValidateUser(user))
                return Unauthorized();
            return Ok(new
            {
                Token = await _authentication.CreateToken()
            });
        }


    }

}
