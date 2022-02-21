using Microsoft.AspNetCore.Identity;

namespace NetCoreStartProject.Domain
{
    public class User : IdentityUser
    {
        public string CustomTag { get; set; }
    }
}
