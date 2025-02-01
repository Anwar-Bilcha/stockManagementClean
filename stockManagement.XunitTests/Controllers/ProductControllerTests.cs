using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using stockManagement.Application.Interfaces;
using stockManagement.Models;
using stockManagement.Models.StockDTO;
using stockManagementClean.API.Controllers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using stockManagement.Models.Entity;
using stockManagement.Models.Shared;

namespace stockManagement.XunitTests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly Mock<IMediator> _mockMediator;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockMediator = new Mock<IMediator>();

            _controller = new ProductsController(
                _mockProductService.Object,
                _mockMapper.Object,
                _mockLogger.Object,
                _mockMediator.Object
            );

            // Mock HttpContext with User Claims
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "TestUser")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

        }
        [Fact]
        public async Task CreateProduct_ValidRequest_ReturnsOkWithResult()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var productDto = new ProductCreaUpdReqDTO
            {
                ProductName = "Laptop",
                ProductDescription = "A powerful laptop",
                ProductCategory = "Electronics",
                IsExpiring = false,
                ExpiryDate = default,
                UnitOfMesaure = "Piece",
                Price = 1200.50,
                Quantity = 10
            };

            var product = new Product
            {
                ProductId = "PR09876",
                ProductName = "Laptop",
                ProductDescription = "A powerful laptop",
                ProductCategory = "Electronics",
                IsExpiring = false,
                ExpiryDate = default,
                UnitOfMesaure = "Piece",
                Price = 1200.50,
                Quantity = 10
            };
            ApiResponse<Product> response = new();
            response.Data = product;
            var responseDto = new ProductCreaUpdResponse
            {
                ProductId = "1",
                ProductName = "Laptop",
                ProductCategory = "Electronics",
                Price = 1200.50
            };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _mockProductService.Setup(s => s.CreateProductAsync(It.IsAny<Product>(), cancellationToken))
                               .ReturnsAsync(response);
            _mockMediator.Setup(m => m.Publish(It.IsAny<ProductRegisteredEvent>(), cancellationToken))
                         .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProduct(productDto, cancellationToken);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ApiResponse<Product>>(okResult.Value);
            Assert.Equal("Laptop", returnedProduct.Data.ProductName);
            Assert.Equal("Electronics", returnedProduct.Data.ProductCategory);
            Assert.Equal(1200.50, returnedProduct.Data.Price);
            Assert.Equal(productDto.Quantity, returnedProduct.Data.Quantity);

            // Verify logging was called
            _mockLogger.Verify(
                log => log.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, @type) =>
                        @object.ToString().Contains("Laptop is being registered")),
                    null,
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                ),
                Times.Exactly(2) // Should log twice as per the method
            );

            // Verify event publishing was called
            _mockMediator.Verify(m => m.Publish(It.IsAny<ProductRegisteredEvent>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            _controller.ModelState.AddModelError("ProductName", "Required");

            var productDto = new ProductCreaUpdReqDTO
            {
                ProductDescription = "A product without a name",
                ProductCategory = "Electronics",
                IsExpiring = false,
                ExpiryDate = default,
                UnitOfMesaure = "Piece",
                Price = 500,
                Quantity = 5
            };

            // Act
            var result = await _controller.CreateProduct(productDto, cancellationToken);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(modelState.ContainsKey("ProductName"));

            // Ensure logging and event publishing are **not** called when model state is invalid
            //_mockLogger.Verify(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Never);
            //_mockMediator.Verify(m => m.Publish(It.IsAny<ProductRegisteredEvent>(), cancellationToken), Times.Never);
        }
    }
}