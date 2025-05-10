using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class IEntity<TId> 
    {
            [Key]
            public TId Id { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public Guid? CreatedBy { get; set; }
            public Guid? UpdatedBy { get; set; }
            public DateTime? DeletedDate { get; set; }
            public bool IsDeleted { get; set; } = false;
    }
}
