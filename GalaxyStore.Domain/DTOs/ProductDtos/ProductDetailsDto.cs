using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.ProductDtos
{
    public class ProductDetailsDto : ProductDto
    {
        public double ProfitRatio { get; set; }
        public int NumberInStock { get; set; }
        public int NumberInStore { get; set; }
    }
}
