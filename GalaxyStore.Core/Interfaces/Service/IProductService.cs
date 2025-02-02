using GalaxyStore.Domain.DTOs.ProductDtos;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDto>> AddProductAsync(CreateProductDto dto);
        Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductDto dto);
        Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<List<ProductDetailsDto>>> GetAllProductsInDetailsAsync();
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id);
    }
}
