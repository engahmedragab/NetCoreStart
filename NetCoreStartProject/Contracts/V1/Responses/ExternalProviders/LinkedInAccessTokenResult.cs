using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Responses.ExternalProviders
{
    public class LinkedInAccessTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

    }
}
