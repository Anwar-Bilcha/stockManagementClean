using AutoMapper;
using AutoMapper.Execution;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.StockDTO;

namespace stockManagementClean.API.Mappers
{
    public class StockManagementMapper : Profile
    {
        public StockManagementMapper()
        {
            CreateMap<ProductCreaUpdReqDTO, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom<ProductResolver>());
        }

    } }

    public class ProductResolver : IValueResolver<ProductCreaUpdReqDTO, Product,string>
    {
        private readonly IProductService _productService;

        public ProductResolver(IProductService productService)
        {
            _productService = productService;
        }

        public string Resolve(ProductCreaUpdReqDTO source, Product destination, string member, ResolutionContext context)
        {
            // Implement your custom mapping logic here using _productService
            // For example:
            //var productId = source.ProductId;
            destination.ProductId = $"PR_{_productService.GetProducts().Data.Count + 1}{source.ProductCategory.Substring(3)}";
            destination.ProductName = source.ProductName;
            destination.ProductDescription = source.ProductDescription;
            destination.ProductCategory = source.ProductCategory;
            destination.IsExpiring = source.IsExpiring;
            destination.ExpiryDate = source.ExpiryDate;
            destination.Price = source.Price;
            destination.Quantity = source.Quantity;
            destination.CreatedBy = (destination.CreatedBy !=null)?destination.CreatedBy: "Admin";
        destination.UpdatedBy = (destination.UpdatedBy != null) ? destination.UpdatedBy : "Admin";
        destination.CreatedOn = DateTime.Now;
        destination.UpdatedOn = DateTime.Now;

        // Since we are resolving the ProductId property, return the generated ProductId
        return destination.ProductId;
        }
    }
