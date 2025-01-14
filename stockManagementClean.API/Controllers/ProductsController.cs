using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.Shared;

namespace stockManagementClean.API.Controllers
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
        [HttpGet]
        public ApiResponse<Product> GetProduct(string id) {
        return _productService.GetProduct(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result= await _productService.CreateProductAsync(product);
                return Ok(result);
            }
            return BadRequest(ModelState);

        }
    }
}
