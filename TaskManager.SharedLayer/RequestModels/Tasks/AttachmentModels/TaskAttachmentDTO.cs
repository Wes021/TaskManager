using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Tasks.AttachmentModels
{
    public class TaskAttachmentDTO
    {
        public string AttactmentType { get; set; }

        public string AttacmentName { get; set; }

        public string AttachementPath { get; set; }
    }
}
