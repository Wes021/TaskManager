using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TasksService(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
        IProjectLookupService projectLookupService, ITasksRepository _tasksRepository,
        ICurrentUserService _currentUser, ITaskStatusRepository _taskStatusRepository,
        ITasksModuleUoW _tasksModuleUoW, IUsersTasks _usersTasks
        ) : ITasksService

    {
        public async Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model, AddMembersToTask MembersModel, List<FileHandlerResponse> fileHandlerResponses)
        {
            var projectInfo = await projectLookupService.GetProjectById(model.ProjectId);


            if (projectInfo.Data == null || projectInfo.Data.IsDeleted == true || projectInfo.Data.IsActive == false)
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


            if (!await _taskStatusRepository.CheckTaskStatusExists(model.TaskStatus))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "StatusDoesNotExists"
                };





            var newTaskResult = Tasks.Domain.Models.Tasks.Create(
    model.Title,
    model.Description,
    model.DueDate,
    model.TaskStatus,
    model.ProjectId,
    _currentUser.UserId
);

            if (!newTaskResult.Succeeded || newTaskResult.Data == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = newTaskResult.Error
                };
            }

            var newTask = newTaskResult.Data;

            // Now call AddMembersToTask on the instance, not the class
            var addMembersResult = newTask.AddMembersToTask(MembersModel.MemberIds, _currentUser.UserId);

            if (!addMembersResult.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = addMembersResult.Error
                };
            }


            var addAttachmentsResult = new GenericDomainResponseModel<List<int>>();

            if (fileHandlerResponses?.Any() == true)
            {
                addAttachmentsResult = newTask.AddAttachmentToTask(fileHandlerResponses, _currentUser.UserId);

                if (!addAttachmentsResult.Succeeded)
                {
                    return new ResponseModel<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = addAttachmentsResult.Error
                    };
                }
            }





            await _tasksRepository.Add(newTask);

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
