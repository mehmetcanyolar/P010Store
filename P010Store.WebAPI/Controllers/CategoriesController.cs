using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Abstract;
using P010Store.Service.Concrete;

namespace P010Store.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryService.GetAll();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await _categoryService.GetProductsByCategoryAsync(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async  Task <Category> Post([FromBody] Category value)
        {
            await _categoryService.AddAsync(value);
            await _categoryService.SaveChangesAsync();
            return value;
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Category value)
        {
            _categoryService.Update(value);
            _categoryService.SaveChanges();
            return NoContent();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var kayit = _categoryService.Find(id);
            if (kayit == null) { return BadRequest(); }
            _categoryService.Delete(kayit);
            _categoryService.SaveChanges();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
