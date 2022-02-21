using NetCoreStartProject.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class Arrangement : BaseEntity
    {
        public bool IsDone { get; set; } = false;
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? TotalPrice { get; set; }
        public bool hasReminder { get; set; } = false;
        public DateTime? BuyDate { get; set; } = DateTime.Now;
        public string Seller { get; set; }
        public string Notes { get; set;}
        public ReminderType ReminderType { get; set; } = ReminderType.None;

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Bride")]
        public int BrideId { get; set; }
        public Bride Bride { get; set; }
    }
}
