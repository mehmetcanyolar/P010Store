using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P010Store.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _productService.GetAllProductsByCategoriesBrandsAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
           return await _productService.GetProductByCategoriesBrandsAsync(id);

           
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<Product> Post([FromBody] Product value)
        {
            await _productService.AddAsync(value);
            await _productService.SaveChangesAsync();
            return value;
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product value)
        {
            _productService.Update(value);
            await _productService.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var kayit = await _productService.FindAsync(id);

            if (kayit == null)
            {
                return BadRequest();
            }

            _productService.Delete(kayit);
            await _productService.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
