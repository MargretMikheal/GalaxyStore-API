using GalaxyStore.Domain.Enums;

namespace GalaxyStore.Domain.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public DateTime CreationDate { get; set; }
        public DateTime? LastTransactionDate { get; set; } 

        public ICollection<Invoice> Invoices { get; set; }
    }
}
