using E_Commerce.BL.Contracts;
using E_Commerce.BL.Features.Orders.DTOs;
using E_Commerce.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class OrderController : ControllerBase
        {
            private readonly IOrderServices _orderService;

            public OrderController(IOrderServices orderService)
            {
                _orderService = orderService;
            }

            
            [HttpPost]
            public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDto)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.AddOrder(orderDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);

        }

         
            [HttpGet("{id}")]
            public async Task<IActionResult> GetOrderById(int id)
            {
            try
            {
                var order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

          
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDTO orderDto)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedOrder = await _orderService.UpdateOrder(id, orderDto);
                return Ok(updatedOrder);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

   
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteOrder(int id)
            {
            try
            {
                var deletedOrderId = await _orderService.DeleteOrder(id);
                return Ok(new { Message = "Ticket deleted successfully.", OrderId = deletedOrderId });
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

           
            [HttpGet("status/{status}")]
            public async Task<IActionResult> FilterByStatus(OrderStatus status)
            {
                var orders = await _orderService.GetOrdersByStatusAsync(status);
                return Ok(orders);
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
        }
}
