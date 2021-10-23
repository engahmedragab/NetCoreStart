using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Responses.ExternalProviders
{
    public class LinkedInEmailUserInfoResult
    {

        [JsonProperty("elements")]
        public Element[] Elements { get; set; }
    }

    public class Element
    {
        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("handle~")]
        public Handle ElementHandle { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Handle
    {
        [JsonProperty("emailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress { get; set; }

        [JsonProperty("phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public PhoneNumber PhoneNumber { get; set; }
    }

    public class PhoneNumber
    {
        [JsonProperty("number")]
        public string Number { get; set; }
    }
    
}
