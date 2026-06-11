using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;

namespace Tasks.Tasks.Domain.Models
{
    public class TaskAttachments : IEntity, IAuditedEntity
    {
        [Key]
        public int Id { get; set; }


        public int TasksId { get; set; }
        public Tasks Tasks { get; set; }

        public string AttactmentType { get; set; }

        public string AttacmentName { get; set; }

        public string AttachementPath { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }



        private TaskAttachments() { }

        internal TaskAttachments(int taskId, string attactmentType, string attacmentName, string attachementPath, int? createdUser)
        {
            TasksId = taskId;
            AttactmentType = attactmentType;
            AttacmentName = attacmentName;
            CreatedUser = createdUser;
            CreatedDate = DateTime.Now;
            IsDeleted = false;
            IsActive = true;

        }
    }
}
