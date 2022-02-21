using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace NetCoreStartProject.Domain
{
    public class Category : BaseEntity
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [NotMapped]
        public string Name { get { return Thread.CurrentThread.CurrentCulture.Name == "en" ? NameEn : NameAr; } }

        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
