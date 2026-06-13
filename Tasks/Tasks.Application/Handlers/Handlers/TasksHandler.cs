using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Application.Handlers.IHandlers;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Application.Handlers.Handlers
{
    public class TasksHandler(ICurrentUserService _currentUserService, IStringLocalizer<SharedResource> _localizer,
        IFileManager _fileManager, ITasksService _tasksService
        ) : ITasksHandler
    {
        public async Task<ResponseModel<bool>> AddNewTask(AddNewTaksDTO model, AddMembersToTask MembersModel, FileValidateRequest FilesModel)
        {
            if (model == null)
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };


            if (MembersModel.MemberIds.Count < 1 && model.ProjectStatus != 2)
            {
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["AtLeastOneMemberMustBeAddedToTask"]
                };
            }


            if (_currentUserService.Role !=
                SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };


            if (FilesModel.Files.Count > 3)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["YouExceededTheLimiteAmountOfAllowedFiles"]
                };
            }

            var FilesResponse = new ResponseModel<List<FileHandlerResponse>>();

            if (FilesModel.Files.Any())
            {
                FilesResponse = _fileManager.FileHandlerService(FilesModel.Files);
            }



            var response = await _tasksService.AddNewTask(model, MembersModel, FilesResponse.Data);

            return response;
        }
    }
}
