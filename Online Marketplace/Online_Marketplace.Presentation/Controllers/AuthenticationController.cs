using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IUserServices;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authentication;


        public AuthenticationController(IAuthService authentication)
        {
            _authentication = authentication;
        }

       
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [SwaggerOperation(Summary = "Authenticate user and create token", Description = "Authenticate user and create token.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Token created successfully.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid user credentials.")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var response = await _authentication.ValidateUser(user);

            if (!response.Success)
                return BadRequest(response);

            return Ok(new { Token = await _authentication.CreateToken() });
        }
    }
}
