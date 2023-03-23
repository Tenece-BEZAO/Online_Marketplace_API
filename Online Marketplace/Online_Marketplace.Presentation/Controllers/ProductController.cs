using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Marketplace.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet(Name = "CreateProduct")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateProduct()
        {
            return null;
        }

    }


}
