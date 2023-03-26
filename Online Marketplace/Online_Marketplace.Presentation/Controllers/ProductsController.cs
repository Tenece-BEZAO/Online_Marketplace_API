using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Marketplace.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            return null;
        }

    }


}
