using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Requests
{
    public class UserHasPasswordRequest
    {
        public string UserId { get; set; }
    }
}
