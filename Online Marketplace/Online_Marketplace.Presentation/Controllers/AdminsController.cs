using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IProfileServices;
using Online_Marketplace.BLL.Interface.IServices;
using Online_Marketplace.Shared.DTOs;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Register a new admin.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The admin was registered successfully.", typeof(AdminForRegistrationDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request was invalid.")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminForRegistrationDto adminForRegistration)
        {
            var response = await _adminServices.RegisterAdmin(adminForRegistration);
            return Ok(response);
        }

        [HttpPost("createProfile")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Create an admin profile.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The admin profile was created successfully.", typeof(AdminProfileDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The user is unauthorized.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "There was an internal server error.")]
        public async Task<IActionResult> CreateProfile([FromBody] AdminProfileDto adminProfile)
        {

            var response = await _adminProfileServices.CreateProfile(adminProfile);
            return Ok(response);

        }


        [HttpPost("updateProfile")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update an admin profile.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The admin profile was updated successfully.", typeof(AdminProfileDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The user is unauthorized.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "There was an internal server error.")]
        public async Task<IActionResult> UpdateProfile([FromBody] AdminProfileDto adminProfile)
        {

            var response = await _adminProfileServices.UpdateProfile(adminProfile);
            return Ok(response);


        }
    }
}
