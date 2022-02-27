using NetCoreStartProject.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class Offer : BaseEntity
    {
        [Required]
        public string TitleAr { get; set; }
        [Required]
        public string TitleEn { get; set; }
        [Required]
        public string SubTitleAr { get; set; }
        [Required]
        public string SubTitleEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public double Amount { get; set; }
        public DiscountType DiscountType  { get; set; }
        public int? BuysNumber { get; set; }
        public int? GetsNumber { get; set; }
        public OfferType Type { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
