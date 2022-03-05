using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreStartProject.Domain
{
    public abstract class Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public Boolean IsDeleted { get; set; } = false;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        public string? Slug { get; set; } = String.Empty;
    }

  public abstract class BaseEntity : Base
    {

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
     
        [Required]
        [ForeignKey("CreatedByUser")]
        public Guid CreatedBy { get; set; }
        [Required]
        [ForeignKey("LastModifiedByUser")]
        public Guid LastModifiedBy { get; set; }
       
        public User CreatedByUser { get; set; }

        public User LastModifiedByUser { get; set; }

      
       

        public  virtual void SetCreatedBy(Guid createdBy)
        {
            if(createdBy == Guid.Empty)
            {
                throw new InvalidOperationException();
            }
            CreatedBy = createdBy;
            CreationDate = DateTime.Now;
        }

        public virtual void SetLastModifiedBy(Guid lastModifiedBy)
        {
            if (lastModifiedBy == Guid.Empty)
            {
                throw new InvalidOperationException();
            }
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = DateTime.Now;
        }




    }
}
