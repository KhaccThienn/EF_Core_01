using EF_Core_01.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Core_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly ICategoryService _service;

        public CategoryController(ModelContext context, ICategoryService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _service.GetCategories();
        }

        [HttpPost]
        public Category Insert([FromBody] Category category)
        {
            return _service.Insert(category);
        }

        [HttpPut("{id}")]
        public Category Update(int id, [FromBody] Category category)
        {
            return _service.Update(id, category);
        }

        [HttpDelete("{id}")]
        public Category Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
