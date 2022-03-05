using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class City : BaseLookup
    {
        public string? Code { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public string? FlagCode { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
