using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.Shared;
using stockManagement.Models.StockDTO;
using stockManagementClean.API.Utilities;

namespace stockManagementClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("StockManagementCORSPolicy")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _publisher;
        public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger, IMediator mediator)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _publisher = mediator;
        }
        [HttpGet]
        public ApiResponse<Product> GetProduct(string id) {
        return _productService.GetProduct(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCreaUpdReqDTO product, CancellationToken cancellationToken)
        {
            var userName = HttpContext.User.Claims.First().Value??"Anonymous";
            _logger.LogError($"{product.ProductName} is being registered that Expires on {product.ExpiryDate} ");
            if (ModelState.IsValid)
            {
                var productToAdd = _mapper.Map<Product>(product);
                var result= await _productService.CreateProductAsync(productToAdd, cancellationToken);
                await _publisher.Publish<ProductRegisteredEvent>(new ProductRegisteredEvent(product.ProductName, TwilioConfigurations.OwnersNumber, userName), cancellationToken);

                _logger.LogError($"{product.ProductName} is being registered that Expires on {product.ExpiryDate} ");

                return Ok(result);
            } 
            return BadRequest(ModelState);

        }
    }
}
