using GalaxyStore.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxyStore.Domain.DTOs
{
    public class EditUserRoleDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "At least one role is required.")]
        [MinLength(1, ErrorMessage = "At least one role must be provided.")]
        public List<UserRole> Roles { get; set; }
    }
}
