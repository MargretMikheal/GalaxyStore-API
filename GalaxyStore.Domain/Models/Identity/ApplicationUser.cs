using GalaxyStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace GalaxyStore.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public Gander Gander { get; set; }
        //public string Password { get; set; }
    }
}
