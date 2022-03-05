using NetCoreStartProject.Enums;
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

        public bool IsDone { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Bride { get; set; }
        public string Groom { get; set; }
        
        public DateTime WeddingDate { get; set; }
        public DateTime? EngagementDate { get; set; }
        public DateTime? HennaDate { get; set; }
        public DateTime? CrownDate { get; set; }

        
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }

        public string Area { get; set; }
        public string Weddinghole { get; set; }
        public string WeddingAddress { get; set; }

        //[ForeignKey("Region")]
        //public int? RegionId { get; set; }
        //public Region Region { get; set; }

        public ResponseFrom ResponseFrom { get; set; }


    }
}
