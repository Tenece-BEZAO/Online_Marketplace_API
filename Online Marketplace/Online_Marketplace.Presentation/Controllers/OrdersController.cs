using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Implementation;
using Online_Marketplace.BLL.Interface;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Marketplace.Presentation.Controllers
{
    [ApiController]
    [Route("marketplace/[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly ILoggerManager _logger;



        public OrdersController(IOrderService orderService, ILoggerManager logger)
        {


            _orderService = orderService;
            _logger = logger;

        }


        [Authorize(Roles = "Buyer")]
        [HttpGet ("view-order-history")]
        public async Task<IActionResult> BuyerOrderHistory() {

        

            try
            {
                var orders =  await _orderService.GetOrderHistoryAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(BuyerOrderHistory)} controller action {ex}");


                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }


        }
 



    }
}
