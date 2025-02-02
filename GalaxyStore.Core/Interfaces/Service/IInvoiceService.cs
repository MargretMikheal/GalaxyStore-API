using GalaxyStore.Domain.DTOs.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<int>> CreateSupplierInvoiceAsync(CreateInvoiceDto invoiceDto);
        Task<ServiceResponse<List<LatestSupplierTransactionDto>>> GetLatestSupplierTransactionsAsync();
        Task<ServiceResponse<int>> CreateCustomerInvoiceAsync(CreateCustomerInvoiceDto invoiceDto);

    }
}
