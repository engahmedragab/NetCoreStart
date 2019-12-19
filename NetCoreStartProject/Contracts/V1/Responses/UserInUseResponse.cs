using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Responses
{
    public class UserInUseResponse
    {
        public bool UserInUse { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
