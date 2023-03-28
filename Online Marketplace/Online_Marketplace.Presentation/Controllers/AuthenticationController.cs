using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Implementation;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.DAL.Entities.Models;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;

namespace Online_Marketplace.Presentation.Controllers
{

    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthService _authentication;
        private readonly UserManager<User> _userManager;



        public AuthenticationController(IAuthService authentication, UserManager<User> userManager)
        {
            _authentication = authentication;
            _userManager = userManager;
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var response = await _authentication.ValidateUser(user);

            if (!response.Success)
                return BadRequest(response);
            return Ok(new
            {
                Token = await _authentication.CreateToken()
            });
        }



        /* [HttpPost("login")]
         [ServiceFilter(typeof(ValidationFilterAttribute))]
         public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
         {
             var response = await _authentication.ValidateUser(user);

             if (!response.Success)
                 return BadRequest(response);
             return Ok(new
             {
                 Token = await _authentication.gener ()
             });
         }*/

    }

}
