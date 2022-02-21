using System.Collections.Generic;

namespace NetCoreStartProject.Contracts.V1.Responses.Identity
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}