using GalaxyStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public Gander Gander { get; set; }
        public string Password { get; set; }
    }
}
