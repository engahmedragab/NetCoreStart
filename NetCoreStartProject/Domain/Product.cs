using NetCoreStartProject.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class Product : BaseEntity
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string BioAr { get; set; }
        [Required]
        public string BioEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; } = false;
        public double? Rate { get; set; }
        public int? Likes { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string Url { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public DiscountType? DiscountType { get; set; }
        public int? BuysNumber { get; set; }
        public int? GetsNumber { get; set; }

        [ForeignKey("Provider")]
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
