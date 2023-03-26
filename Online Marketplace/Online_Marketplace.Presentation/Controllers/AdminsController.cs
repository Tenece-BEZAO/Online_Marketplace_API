using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminsController : ControllerBase
    {

        private readonly IAdminServices _adminServices;


        public AdminsController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }



        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminForRegistrationDto adminForRegistration)
        {

            var response = await _adminServices.RegisterAdmin(adminForRegistration);

            return Ok(response);

        }
    }
}
