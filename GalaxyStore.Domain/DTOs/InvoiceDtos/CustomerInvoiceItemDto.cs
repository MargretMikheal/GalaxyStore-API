using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.InvoiceDtos
{
    public class CustomerInvoiceItemDto
    {
        public int ProductId { get; set; }
        public List<string> Barcodes { get; set; }

    }
}
