using GalaxyStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GalaxyStore.Domain.DTOs.AuthDtos
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 2 and 50 characters.", MinimumLength = 2)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Role must be a valid UserRole.")]
        public UserRole Role { get; set; }

        [Required(ErrorMessage = "EmployeeId is required.")]
        [StringLength(20, ErrorMessage = "EmployeeId must not exceed 20 characters.")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be between 2 and 100 characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gander), ErrorMessage = "Gender must be a valid value.")]
        public Gander Gender { get; set; }
    }
}
