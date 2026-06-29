
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TaskManager.SharedLayer.RequestModels.Tasks.AttachmentModels
{
    public class FileRequestDTO
    {
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
