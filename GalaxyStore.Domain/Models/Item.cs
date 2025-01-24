using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Barcode { get; set; } 
        public bool IsInStock { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedDate { get; set; } 
        public int ProductId { get; set; }
        public int SupplierId { get; set; } 

       
        public Product Product { get; set; }
        public Partner Supplier { get; set; } 
    }

}
