using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;

namespace NetCoreStartProject.Domain
{
    public class BaseLookup : Base
    {
        
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [NotMapped]
        public string Name { get { return Thread.CurrentThread.CurrentCulture.Name == "en" ? NameEn : NameAr; } }

        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
       

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        [ForeignKey("LastModifiedBy")]
        public Guid? LastModifiedBy { get; set; }

        public User CreatedByUser { get; set; }

        public User LastModifiedByUser { get; set; }

        

    }
}
