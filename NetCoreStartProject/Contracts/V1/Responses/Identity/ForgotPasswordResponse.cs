using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Responses.Identity
{
    public class ForgotPasswordResponse
    {
        public string ForgetToken { get; set; }
    }
}
