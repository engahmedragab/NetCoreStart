using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Options
{
    public class LinkedInAuthSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string RedirectUri { get; set; }
    }
}
