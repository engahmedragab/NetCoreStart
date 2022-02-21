using System.ComponentModel.DataAnnotations;

namespace NetCoreStartProject.Contracts.V1.Requests.Identity
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}