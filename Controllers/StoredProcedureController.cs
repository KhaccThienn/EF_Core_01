using EF_Core_01.Models;
using EF_Core_01.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoredProcedureController : ControllerBase
    {
        private readonly StoredProcedureServices storedProcedureServices;
        private readonly IConfiguration _config;
        public StoredProcedureController(IConfiguration config)
        {
            _config = config;
            storedProcedureServices = new StoredProcedureServices(_config);
        }

        [HttpGet("GetAllProduct")]
        public async Task<IEnumerable<Product>> GetProductsAsync(int? categoryId)
        {
            return await storedProcedureServices.GetProductsAsync(categoryId);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int? id)
        {
            storedProcedureServices.DeleteProductById(id);
            return NoContent();
        }

        [HttpPost("InsertProduct")]
        public IActionResult InsertProduct([FromBody] Product model)
        {
            storedProcedureServices.InsertProduct(model);
            return Ok();
        }
    }
}
