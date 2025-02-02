using GalaxyStore.Core.Helper;
using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.ProductDtos;
using GalaxyStore.Domain.Models;
using Mapster;

namespace GalaxyStore.Core.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<ProductDto>> AddProductAsync(CreateProductDto dto)
        {
            const string imageSubFolder = "products";

            var imagePath = dto.ProductPhoto != null
                ? await FileHelper.SaveFileAsync(dto.ProductPhoto, imageSubFolder)
                : null;

            var product = dto.Adapt<Product>();
            product.ProductPhoto = imagePath;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            var productDto = product.Adapt<ProductDto>();
            productDto.ProductPhoto = FileHelper.ReadFileAsBytes(product.ProductPhoto);

            return new ServiceResponse<ProductDto>
            {
                Success = true,
                Data = productDto,
                Message = "Product added successfully."
            };
        }

        public async Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Product not found." };

            if (dto.ProductPhoto != null)
            {
                const string imageSubFolder = "products";
                product.ProductPhoto = await FileHelper.SaveFileAsync(dto.ProductPhoto, imageSubFolder);
            }

            dto.Adapt(product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();

            var productDto = product.Adapt<ProductDto>();
            productDto.ProductPhoto = FileHelper.ReadFileAsBytes(product.ProductPhoto);

            return new ServiceResponse<ProductDto>
            {
                Success = true,
                Data = productDto,
                Message = "Product updated successfully."
            };
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var productDtos = products.Adapt<List<ProductDto>>();

            foreach (var productDto in productDtos)
            {
                var product = products.FirstOrDefault(p => p.Id == productDto.Id);
                productDto.ProductPhoto = FileHelper.ReadFileAsBytes(product?.ProductPhoto);
            }

            return new ServiceResponse<List<ProductDto>>
            {
                Success = true,
                Data = productDtos,
                Message = "Products retrieved successfully."
            };
        }

        public async Task<ServiceResponse<List<ProductDetailsDto>>> GetAllProductsInDetailsAsync()
        {
            var products = await _unitOfWork.Products.GetAllWithIncludesAsync(p => p.Inventories);
            var productDetails = products.Select(p =>
            {
                var dto = p.Adapt<ProductDetailsDto>();
                dto.ProductPhoto = FileHelper.ReadFileAsBytes(p.ProductPhoto);
                dto.ProfitRatio = (double)((double)p.SellingPrice - (double)p.CurrentPurchase) / (double)p.CurrentPurchase * 100;
                dto.NumberInStock = p.Inventories?.Sum(i => i.NumOfProductInStock) ?? 0;
                dto.NumberInStore = p.Inventories?.Sum(i => i.NumOfProductInStore) ?? 0;
                return dto;
            }).ToList();

            return new ServiceResponse<List<ProductDetailsDto>>
            {
                Success = true,
                Data = productDetails,
                Message = "Product details retrieved successfully."
            };
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return new ServiceResponse<ProductDto> { Success = false, Message = "Product not found." };

            var productDto = product.Adapt<ProductDto>();
            productDto.ProductPhoto = FileHelper.ReadFileAsBytes(product.ProductPhoto);

            return new ServiceResponse<ProductDto>
            {
                Success = true,
                Data = productDto,
                Message = "Product retrieved successfully."
            };
        }

        
    }
}
