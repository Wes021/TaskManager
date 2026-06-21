using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;

namespace Tasks.Tasks.Application.Handlers.IHandlers
{
    public interface ITaskCommentsHandler
    {
        Task<ResponseModel<bool>> AddNewComment(int TaskId, AddNewComment model);
    }
}
