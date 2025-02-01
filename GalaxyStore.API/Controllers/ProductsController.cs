using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GalaxyStore.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto dto)
        {
            var response = await _productService.AddProductAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
        {
            var response = await _productService.UpdateProductAsync(id, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProductsAsync();
            return response.Success ? Ok(response) : NotFound(response);
        }

        
        [HttpGet("details")]
        public async Task<IActionResult> GetAllProductsInDetails()
        {
            var response = await _productService.GetAllProductsInDetailsAsync();
            return response.Success ? Ok(response) : NotFound(response);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        
        //[HttpGet("barcode/{barcode}")]
        //public async Task<IActionResult> GetProductByBarcode(string barcode)
        //{
        //    var response = await _productService.GetProductByBarcodeAsync(barcode);
        //    return response.Success ? Ok(response) : NotFound(response);
        //}
    }
}
