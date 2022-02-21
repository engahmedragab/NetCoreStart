using NetCoreStartProject.Enums.Item;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreStartProject.Domain
{
    public class ItemDetailsHistory : BaseEntity
    {
        public string Utilization { get; set; }
        public Importance Importance { get; set; }
        public Priority priority { get; set; }
        public string Usage { get; set; }
        public int Quantity { get; set; }
        public string Represent { get; set; }
        public RepresentType RepresentType { get; set; }
        public string AvailableTypes { get; set; }
        public string PopularBrands { get; set; }
        public string Places { get; set; }
        public double? LowPrice { get; set; }
        public double? HighPrice { get; set; }
        public double Price { get; set; }
        public string Additions { get; set; }

        [ForeignKey("ItemDetails")]
        public int ItemDetailsId { get; set; }
        public ItemDetails ItemDetails { get; set; }
    }
}
