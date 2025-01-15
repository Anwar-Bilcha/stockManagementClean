using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.Shared;
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
        public ProductService(StockDbContext context) {
        _context = context;
        }
        async Task<ApiResponse<Product>> IProductService.CreateProductAsync(Product product)
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
            throw new NotImplementedException();
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
