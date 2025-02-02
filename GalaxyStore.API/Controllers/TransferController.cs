using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.TransferDtos;
using GalaxyStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyStore.API.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost("to-store")]
        public async Task<IActionResult> TransferToStore([FromBody] TransferToStoreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _transferService.TransferToStoreAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("to-stock")]
        public async Task<IActionResult> TransferToStock([FromBody] TransferToStockDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _transferService.TransferToStockAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
