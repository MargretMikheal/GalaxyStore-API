using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.InvoiceDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("supplier-invoice")]
        public async Task<IActionResult> CreateSupplierInvoice([FromBody] CreateInvoiceDto invoiceDto)
        {
            var response = await _invoiceService.CreateSupplierInvoiceAsync(invoiceDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("latest-transactions")]
        public async Task<IActionResult> GetLatestSupplierTransactions()
        {
            var result = await _invoiceService.GetLatestSupplierTransactionsAsync();
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
