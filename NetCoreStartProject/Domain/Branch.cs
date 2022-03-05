using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class Branch : BaseEntity
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }

        //[ForeignKey("Region")]
        //public int? RegionId { get; set; }
        //public Region Region { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public bool IsActive { get; set; }
        public double? Rate { get; set; }
        public int? Likes { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string LinkWebsite { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkInstgram { get; set; }
        public string LinktWitter { get; set; }
        public string LinkWhatsApp { get; set; }
        
        [ForeignKey("Provider")]
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
