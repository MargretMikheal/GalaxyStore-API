using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public decimal CurrentPurchase { get; set; }
        public decimal SellingPrice { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public byte[] ProductPhoto
        {
            get; set;
        }
    }
}