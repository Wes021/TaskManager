using TaskManager.SharedLayer.RequestModels.Tasks.CommentsModel;
using TaskManager.SharedLayer.ResponseModel;

namespace Tasks.Tasks.Application.Handlers.IHandlers
{
    public interface ITaskCommentsHandler
    {
        Task<ResponseModel<bool>> AddNewComment(int TaskId, AddNewComment model);

        Task<ResponseModel<bool>> DeleteComment(int commentId, int TaskId);
    }
}
