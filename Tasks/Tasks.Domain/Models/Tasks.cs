using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;

namespace Tasks.Tasks.Domain.Models
{
    public class Tasks : IEntity, IAuditedEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }



        public DateTime DueDate { get; set; }

        public int TasksStatusId { get; set; }
        public TasksStatus TasksStatus { get; set; }


        public TaskAttachments TaskAttachments { get; set; }


        public DateTime CreatedDate { get ; set ; }
        public int? CreatedUser { get ; set ; }
        public DateTime? ModifiedDate { get ; set ; }
        public int? ModifiedUser { get ; set ; }
        
        public bool IsDeleted { get ; set ; }
        public bool IsActive { get ; set ; }
    }
}
