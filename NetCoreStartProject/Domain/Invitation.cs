using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class Invitation : Base
    {
        public Invitation()
        {
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Bride { get; set; }
        public string Groom { get; set; }
        
        public DateTime WeddingDate { get; set; }
        public DateTime? EngagementDate { get; set; }
        public DateTime? HennaDate { get; set; }
        public DateTime? CrownDate { get; set; }

        public string WeddingVenue { get; set; }
        public string WeddingVenueAddress { get; set; }
        
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }

        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public Region Region { get; set; }


    }
}
