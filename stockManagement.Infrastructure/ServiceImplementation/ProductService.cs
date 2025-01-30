using MediatR;
using Microsoft.Extensions.Logging;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.Shared;
using stockManagementClean.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Infrastructure.ServiceImplementation
{
    public class ProductService : IProductService
    {
        private readonly StockDbContext _context;
        private readonly IMediator _publisher;
        private readonly ILogger<ProductService> _logger;

        public ProductService(StockDbContext context, IMediator publisher, ILogger<ProductService> logger) {
        _context = context;
        _publisher = publisher;
        _logger = logger;
        }
        async Task<ApiResponse<Product>> IProductService.CreateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            ApiResponse<Product> userResult = new();
            try
            {
                 _context.Products.Add(product);
                var result = await _context.SaveChangesAsync();
                userResult = new();
                userResult.errorMessage = "";
                userResult.isSuccessfullyCompleted = true;
                userResult.Data = product;
                await _publisher.Publish<ProductRegisteredEvent>(new ProductRegisteredEvent(product.ProductName, TwilioConfigurations.OwnersNumber), cancellationToken);
                return userResult;
            }
            catch (Exception ex)
            {
                userResult = new();
                userResult.errorMessage = ex.InnerException.Message;
                userResult.isSuccessfullyCompleted = false;
                userResult.Data = new();
                return userResult;
            }   
        }

        Task<ApiResponse<Product>> IProductService.DeleteProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        ApiResponse<Product> IProductService.GetProduct(string productId)
        {
            var data = _context.Products.Find(productId);
            ApiResponse<Product> result = new ApiResponse<Product> { Data = data == null ? new() : data, errorMessage = " ", isSuccessfullyCompleted = true, generatedOn = DateTime.Now };
        return result;
        }

        ApiResponse<List<Product>> IProductService.GetProducts()
        {
            return new ApiResponse<List<Product>>() { Data = _context.Products.ToList(), errorMessage="", isSuccessfullyCompleted = true, generatedOn = DateTime.Now };
        }

        Task<ApiResponse<Product>> IProductService.UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
