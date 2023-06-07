using AutoMapper;
using Data_Access_Layer.Data;
using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.DTO;
using Business_Logic_Layer.BLLs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private ProductBLL _BLL;
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context, IMapper mapper)
        {
            _BLL = new ProductBLL(context, mapper);
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetProducts()
        {
            var products = _BLL.GetProducts();

            if (products == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetProduct(int id)
        {
            if (_context.Products == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _BLL.GetProduct(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            
            if (productDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            bool product = _BLL.UpdateProduct(productDto);

            if (!product)
                return NotFound();

            return Ok("Product " + productDto.ProductId + " was updated successfully");
        }

        // POST: api/Products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateProduct([FromBody] ProductCreateDto newproduct)
        {
            bool product = _BLL.CreateProduct(newproduct);

            if (newproduct == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!product)
                return Conflict();

            return Ok("Successfully created a new Product"); //I can also try to print the product id
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!_BLL.ProductExists(id))
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Successfully deleted Product with id: "+ product.ProductId);
        }

    }
}
