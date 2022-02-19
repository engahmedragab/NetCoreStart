using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Requests
{
    public class MailConfirmationRequest
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
