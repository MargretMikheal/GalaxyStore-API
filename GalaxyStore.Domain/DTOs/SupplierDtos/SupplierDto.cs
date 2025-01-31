using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.SupplierDtos
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public byte[] Image { get; set; }
        public byte[] IdImage { get; set; }
    }
}
