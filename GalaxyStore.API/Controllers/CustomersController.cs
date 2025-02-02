using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.DTOs.CustomerDtos;
using GalaxyStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GalaxyStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager) + "," + nameof(UserRole.Sales))]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _customerService.GetAllCustomersAsync();
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _customerService.AddCustomerAsync(dto);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }
    }
}
