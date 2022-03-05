using System.Collections.Generic;

namespace NetCoreStartProject.Domain
{
    public class Country : BaseLookup
    {
        public string? Code { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public string? FlagCode { get; set; }
        public string? CapitalNames { get; set; }
        public string? CurrencyCodes { get; set; }
        public string? PhoneCodes { get; set; }
        public string? alpha2 { get; set; }
        public string? alpha3 { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
