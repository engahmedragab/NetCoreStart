using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Requests
{
    public class ResetPasswordRequest
    {
        public string UserEmail { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }
    }
}
