using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IFileManager
    {
        public List<ResponseModel<List<FileHandlerResponse>>> FileHandlerService (List<FileRequestDTO> model)
    }
}
