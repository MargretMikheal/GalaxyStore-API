using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.InvoiceDtos
{
    public class LatestSupplierTransactionDto
    {
        public string SupplierName { get; set; }
        public decimal TotalPay { get; set; }
        public DateTime Date { get; set; }
    }
}
