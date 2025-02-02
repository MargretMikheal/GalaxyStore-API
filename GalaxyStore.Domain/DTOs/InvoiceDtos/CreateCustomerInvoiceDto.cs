using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.InvoiceDtos
{
    public class CreateCustomerInvoiceDto
    {
        public int CustomerId { get; set; }
        public int WarhouseId { get; set; }
        public List<CustomerInvoiceItemDto> Items { get; set; }
    }
}
