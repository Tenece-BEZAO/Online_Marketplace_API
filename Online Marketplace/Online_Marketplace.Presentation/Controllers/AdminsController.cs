using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminsController : ControllerBase
    {

        private readonly IAdminServices _adminServices;
        private readonly IAdminProfileServices _adminProfileServices;

        public AdminsController(IAdminServices adminServices, IAdminProfileServices adminProfileServices)
        {
            _adminServices = adminServices;
            _adminProfileServices = adminProfileServices;
        }



        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminForRegistrationDto adminForRegistration)
        {

            var response = await _adminServices.RegisterAdmin(adminForRegistration);

            return Ok(response);

        }


        [HttpPost("createProfile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProfile([FromBody] AdminProfileDto adminProfile)
        {
            try
            {
                var response = await _adminProfileServices.CreateProfile(adminProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }


        [HttpPost("updateProfile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProfile([FromBody] AdminProfileDto adminProfile)
        {
            try
            {
                var response = await _adminProfileServices.UpdateProfile(adminProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
