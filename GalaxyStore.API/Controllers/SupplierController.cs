using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.SupplierDtos;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var response = await _supplierService.GetAllSuppliersAsync();
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var response = await _supplierService.GetSupplierByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier(CreateSupplierDto dto)
        {
            var response = await _supplierService.AddSupplierAsync(dto);
            if (!response.Success) return BadRequest(response);
            return CreatedAtAction(nameof(GetSupplierById), new { id = response.Data.Id }, response);
        }
    }
}
