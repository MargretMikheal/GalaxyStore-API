using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace GalaxyStore.Domain.DTOs.SupplierDtos
{
    public class CreateSupplierDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; } 

        [Required(ErrorMessage = "ID Image is required.")]
        public IFormFile IdImage { get; set; } 
    }
}
