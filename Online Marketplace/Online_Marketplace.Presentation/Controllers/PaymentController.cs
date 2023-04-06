using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("marketplace/[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService _paymentService;
        private readonly ILoggerManager _logger;

        public PaymentController(IPaymentService paymentService, ILoggerManager logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

     
        [HttpPost("verifypayment")]
        [SwaggerOperation(Summary = "Verifies a payment and updates the order status.")]
        [SwaggerResponse(StatusCodes.Status200OK, "The payment was successfully verified.", typeof(void))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The payment verification failed.", typeof(void))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while verifying the payment.", typeof(void))]
        public async Task<IActionResult> VerifyPayment([FromQuery] string referenceCode)
        {
           
            var success = await _paymentService.VerifyPaymentAndUpdateOrderStatus(referenceCode);

            if (success)
            {
                return Ok(new { message = "Payment verified" });
            }
            else
            {
                return BadRequest(new { message = "Payment verification failed" });
            }
            
        }
    }
}