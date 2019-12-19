using System.Collections.Generic;

namespace NetCoreStartProject.Controllers.V1
{
    internal class UserHasPasswordResponse
    {
        public bool UserHasPassword { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}