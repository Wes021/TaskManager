using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IFileManager
    {
        public ResponseModel<List<FileHandlerResponse>> FileHandlerService(List<FileRequestDTO> model);
    }
}
