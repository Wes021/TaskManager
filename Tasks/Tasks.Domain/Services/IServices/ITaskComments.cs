using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;

namespace Tasks.Tasks.Domain.Services.IServices
{
    public interface ITaskComments
    {
        Task<ResponseModel<bool>> AddNewComment(int TaskId, AddNewComment model);

        Task<ResponseModel<bool>> DeleteComment(int commentId, int TaskId);


    }
}
