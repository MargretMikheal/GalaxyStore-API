using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Domain.DTOs.CustomerDtos
{
    public class CustomerDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public string Phone { get; set; }
       
    }
}
