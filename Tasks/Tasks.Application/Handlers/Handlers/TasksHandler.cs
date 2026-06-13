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
        public async Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model)
        {
            if (model == null)
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };


            if (model.MembersModel.MemberIds.Count < 1 && model.TaskStatus != 2)
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




            var FilesResponse = new ResponseModel<List<FileHandlerResponse>>();

            if (model.Files?.Any() == true)
            {

                if (model.Files.Count > 3)
                {
                    return new ResponseModel<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = _localizer["YouExceededTheLimiteAmountOfAllowedFiles"]
                    };
                }


                FilesResponse = _fileManager.FileHandlerService(model.Files);

                if (!FilesResponse.Success)
                {
                    return new ResponseModel<bool>
                    {
                        Success = false,
                        Message = FilesResponse.Message
                    };
                }
            }



            var response = await _tasksService.AddNewTask(model, model.MembersModel, FilesResponse.Data);

            return response;
        }
    }
}
