using AutoMapper;
using Business_Logic_Layer.BLLs;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private OrderBLL _BLL;

        public OrderController(NorthwindContext context, IMapper mapper)
        {
            _BLL = new OrderBLL(context, mapper);
        }

        // GET: api/Orders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetOrders()
        {
            var orders = await _BLL.GetOrders();

            if (orders == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetOrder(int id)
        {
            var order = await _BLL.GetOrder(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // GET: api/Orders/getByCustomer/ONE
        [HttpGet("getByCustomer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetOrdersByCustomer(string customerId)
        {
            var orders = await _BLL.GetOrdersByCustomer(customerId);
            // Need to add an id it exists
            if (orders == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        // GET: api/Orders/getByCustomerAndEmployee/ONE/5
        [HttpGet("getByCustomerAndEmployee/{customerId}/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            var orders = _BLL.GetByCustomerAndEmployee(customerId, employeeId);
            // Need to add an id it exists
            if (orders == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            if (id != orderDto.OrderId)
                return BadRequest();

            if (orderDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            bool order = _BLL.UpdateOrder(orderDto);

            if (!order)
                return NotFound();

            return Ok("Order " + id + " was updated successfully");
        }

        // POST: api/Orders
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateOrder([FromBody] OrderCreateDto neworder)
        {

            if (neworder == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            bool order = _BLL.CreateOrder(neworder);

            if (!order)
                return Conflict();

            return Ok("Successfully created Order"); //I can also try to print the product id
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            bool order = _BLL.DeleteOrder(id);

            if (!order)
                return NotFound();

            return Ok("Successfully deleted Customer with id: " + id);
        }

    }
}
