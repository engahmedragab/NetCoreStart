using System.Collections.Generic;

namespace NetCoreStartProject.Contracts.V1.Responses.Identity
{
    internal class PasswordResponse
    {
        public bool UserHasPassword { get; set; }

        public bool Succses { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}