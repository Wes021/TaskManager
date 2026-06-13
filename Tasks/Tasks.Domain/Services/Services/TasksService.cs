using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TasksService(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
        IProjectLookupService projectLookupService, ITasksRepository _tasksRepository,
        ICurrentUserService _currentUser, ITaskStatusRepository _taskStatusRepository, ITasksModuleUoW _tasksModuleUoW
        ) : ITasksService

    {
        public async Task<ResponseModel<bool>> AddNewTask(AddNewTaksDTO model, AddMembersToTask MembersModel, List<FileHandlerResponse> fileHandlerResponses)
        {
            var projectInfo = await projectLookupService.GetProjectById(model.ProjectId);


            if (projectInfo == null || projectInfo.Data.IsDeleted == true || projectInfo.Data.IsActive == false)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["ProjectNotFoundOrDeactivated"]
                };

            if (await _tasksRepository.ExistsByTitleAsync(model))

                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "TitleAlreadyExists"
                };


            if (await _taskStatusRepository.CheckTaskStatusExists(model.ProjectStatus))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "TitleAlreadyExists"
                };





            var newTaskResult = Tasks.Domain.Models.Tasks.Create(
    model.Title,
    model.Description,
    model.DueDate,
    model.ProjectStatus,
    model.ProjectId,
    _currentUser.UserId
);

            if (!newTaskResult.Succeeded || newTaskResult.Data == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "TaskCreationFailed"
                };
            }

            var newTask = newTaskResult.Data;

            // Now call AddMembersToTask on the instance, not the class
            var newTasksMembers = newTask.AddMembersToTask(MembersModel.MemberIds, _currentUser.UserId);

            if (!newTasksMembers.Succeeded || newTasksMembers.Data == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "TaskCreationFailed"
                };
            }


            var NewTasksAttachements = newTask.AddAttachmentToTask(fileHandlerResponses, _currentUser.UserId);

            if (!NewTasksAttachements.Succeeded || NewTasksAttachements.Data == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "TaskCreationFailed"
                };
            }



            await _tasksModuleUoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["TaskAddedSuccessfully"]
            };
        }
    }
}
