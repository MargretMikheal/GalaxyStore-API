using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        [Required]
        public string Serial { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal CurrentPurchase { get; set; }
        [Required]
        public decimal SellingPrice { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public IFormFile ProductPhoto { get; set; }
    }
}
