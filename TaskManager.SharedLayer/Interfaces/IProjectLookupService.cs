using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IProjectLookupService
    {
        Task<ResponseModel<ProjectInfoDto>> GetProjectById(int Id);

        Task<ResponseModel<List<ProjectInfoDto>>> GetProjectsByIds(
    List<int> ids);
    }
}
