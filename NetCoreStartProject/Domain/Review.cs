using NetCoreStartProject.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class Review : BaseEntity
    {
        public double? Rate { get; set; }
        public int? Likes { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public ReviewType ReviewType { get; set; }
        
        [ForeignKey("ItemDetails")]
        public int? ItemDetailsId { get; set; }
        public ItemDetails ItemDetails { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
