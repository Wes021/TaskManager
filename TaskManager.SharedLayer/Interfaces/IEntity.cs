using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
    }

    public interface IAuditedEntity
    {
        DateTime CreatedDate { get; set; }
        int? CreatedUser { get; set; }
        DateTime? ModifiedDate { get; set; }
        int? ModifiedUser { get; set; }
        
    }
}
