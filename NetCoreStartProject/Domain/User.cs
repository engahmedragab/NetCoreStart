using Microsoft.AspNetCore.Identity;
using System;

namespace NetCoreStartProject.Domain
{
    public class User : IdentityUser<Guid>
    {
        public User() : base()
        {

        }
        public string CustomTag { get; set; }
    }
}
