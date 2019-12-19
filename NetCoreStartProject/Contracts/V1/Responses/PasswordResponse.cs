using System.Collections.Generic;

namespace NetCoreStartProject.Controllers.V1
{
    internal class PasswordResponse
    {
        public bool UserHasPassword { get; set; }

        public bool Succses { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}