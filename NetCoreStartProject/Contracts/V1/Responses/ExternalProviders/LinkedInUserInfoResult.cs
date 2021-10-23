using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreStartProject.Contracts.V1.Responses.ExternalProviders
{
    public class LinkedInUserInfoResult
    {
        [JsonProperty("localizedLastName")]
        public string LocalizedLastName { get; set; }

        [JsonProperty("profilePicture")]
        public ProfilePicture ProfilePicture { get; set; }

        [JsonProperty("firstName")]
        public StName FirstName { get; set; }

        [JsonProperty("lastName")]
        public StName LastName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("localizedFirstName")]
        public string LocalizedFirstName { get; set; }
        public string Email { get; internal set; }
    }

    public partial class StName
    {
        [JsonProperty("localized")]
        public Localized Localized { get; set; }

        [JsonProperty("preferredLocale")]
        public PreferredLocale PreferredLocale { get; set; }
    }

    public partial class Localized
    {
        [JsonProperty("en_US")]
        public string EnUs { get; set; }
    }

    public partial class PreferredLocale
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public partial class ProfilePicture
    {
        [JsonProperty("displayImage")]
        public string DisplayImage { get; set; }
    }
    
}
