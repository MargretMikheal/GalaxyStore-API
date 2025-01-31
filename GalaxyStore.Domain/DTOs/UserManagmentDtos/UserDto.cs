using GalaxyStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GalaxyStore.Domain.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
