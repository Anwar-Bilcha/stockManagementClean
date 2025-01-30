using stockManagement.Models.Entity;
using stockManagement.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Application.Interfaces
{
    public interface IProductService
    {
        ApiResponse<Product> GetProduct(string productId);
        ApiResponse<List<Product>> GetProducts();
        Task<ApiResponse<Product>> UpdateProductAsync(Product product);
        Task<ApiResponse<Product>> CreateProductAsync(Product product, CancellationToken cancellationToken);
        Task<ApiResponse<Product>> DeleteProductAsync(Product product);
        
    }
}
