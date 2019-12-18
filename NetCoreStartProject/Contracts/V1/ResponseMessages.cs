using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1
{
    public static class ResponseMessages
    {

        public const string Version = "v1";

        public const string APIRoot = "API";
        public const string AdminRoot = "API";

        public const string APIBase = APIRoot + "-" + Version + "-";
        public const string ADMINBase = APIRoot + "-" + Version + "-";
        public static class Posts
        {
            public const string GetAll = APIBase + "posts";

          
        }

        public static class Identity
        {
            public const string Login = APIBase + "/identity/login";

          
        }
    }
}
