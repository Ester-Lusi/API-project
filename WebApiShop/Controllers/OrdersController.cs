using Dtos;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            OrderDto order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NoContent();
            }
            return Ok(order);

        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] OrderDto order)
        {
            OrderDto orderResult = await _orderService.AddOrder(order);
            if (orderResult == null)
                return BadRequest();
            //return CreatedAtAction(nameof(GetOrderById), new { id = orderResult.OrderId }, orderResult);
            return Ok(orderResult);
        }

    }
}
