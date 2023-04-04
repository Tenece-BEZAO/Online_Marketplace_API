using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Marketplace.BLL.Implementation;
using Online_Marketplace.BLL.Implementation.MarketServices;
using Online_Marketplace.BLL.Interface.IMarketServices;
using Online_Marketplace.Logger.Logger;
using Online_Marketplace.Shared.DTOs;
using Online_Marketplace.Shared.Filters;
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
        [HttpGet ("buyer-order-history")]
       
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


        [Authorize(Roles = "Seller")]
        [HttpGet("seller-view-orders")]
        public async Task<IActionResult> SellerOrderHistory()
        {

            try
            {
                var orders = await _orderService.GetSellerOrderHistoryAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(SellerOrderHistory)} controller action {ex}");


                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }


        }
        [HttpGet("{id}/status")]
       
        public async Task<IActionResult> GetOrderStatus(int id)
        {
            try
            {
                var orderStatuses = await _orderService.GetOrderStatusAsync(id);

                return Ok(orderStatuses);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while getting order status:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace); 
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                return StatusCode(500, "An error occurred while getting order status. Please try again later.");
            }
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost("checkout/{cartId:int}")]
        
        public async Task<IActionResult> Checkout(int cartId)
        {
            try
            {
                var result = await _orderService.CheckoutAsync (cartId);

                if (result)
                {
                    return Ok("Cart checked out successfully");
                }
                else
                {
                    return BadRequest("Error occurred while checking out cart");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while checking out cart with ID {cartId}: {ex}");

                return StatusCode(500, "An error occurred while checking out cart");
            }
        }


        [Authorize(Roles = "Seller")]
        [HttpGet("{orderId}/receipt")]
        public async Task<FileResult> GenerateReceiptAsync(int orderId)

        {
            try
            {
                var receipt = await _orderService.GenerateReceiptAsync( orderId);

                // Return the receipt as a file
                return File(receipt, "application/pdf", $"receipt_{orderId}.pdf");
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine("An error occurred while generating the receipt:");
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("Inner exception:");
                sb.AppendLine(ex.InnerException?.Message ?? "No inner exception");

                _logger.LogError(sb.ToString());

                throw;
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpPost("updateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatusDto)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(updateOrderStatusDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
