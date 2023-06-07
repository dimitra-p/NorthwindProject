using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.Models;
using Data_Access_Layer.Data;
using AutoMapper;
using Business_Logic_Layer.BLLs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private CustomerBLL _BLL;

        public CustomerController(NorthwindContext context, IMapper mapper)
        {
            _BLL = new CustomerBLL(context, mapper);
        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _BLL.GetCustomers();

            if (customers == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCustomer(string id)
        {

            var customer = await _BLL.GetCustomer(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (customer == null)
                return NotFound();  

            return Ok(customer);
        }

        // GET: api/Customers/bycompany/One
        [HttpGet("bycompany/{companyName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> GetCustomersByCompanyName(string companyName)
        {
            var customers = await _BLL.GetCustomersByCompanyName(companyName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (customers == null)
                return NotFound();

            return Ok(customers);
        }

        // PUT: api/Customers/AAAAA
        //Edits the Customer with id AAAAA
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.CustomerId)
                return BadRequest();

            if (customerDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            bool customer = _BLL.UpdateCustomer(customerDto);

            if (!customer)
                return NotFound();

            return Ok("Customer " + customerDto.CustomerId + " was updated successfully");
        }

        // POST: api/Customers
        // This request creates a new Customer in the database
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerDto newcustomer)
        {

            if (newcustomer == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            bool customer = _BLL.CreateCustomer(newcustomer);

            if (!customer)
                return Conflict();

            return Ok("Successfully created new Customer with id: " + newcustomer.CustomerId);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
     
            if (!ModelState.IsValid)
                return BadRequest();

            bool customer = _BLL.DeleteCustomer(id);

            if (!customer)
                return NotFound();

            return Ok("Successfully deleted Customer with id: " + id);
        }
    }
}
