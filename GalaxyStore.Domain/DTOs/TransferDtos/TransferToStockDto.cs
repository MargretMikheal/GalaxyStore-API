using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.TransferDtos
{
    public class TransferToStockDto
    {
        [Required(ErrorMessage = "At least one barcode is required.")]
        public List<string> Barcodes { get; set; } = new List<string>();
    }
}
