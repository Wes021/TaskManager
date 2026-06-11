using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels.Tasks
{
    public class FileHandlerResponse
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
