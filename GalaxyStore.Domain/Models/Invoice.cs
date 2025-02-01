using GalaxyStore.Domain.Enums;

namespace GalaxyStore.Domain.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public InvoiceType Type { get; set; } 

        public int PartnerId { get; set; }
        public Partner Partner { get; set; }

        public DateTime CreationDate { get; set; }
        public decimal TotalPay { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
