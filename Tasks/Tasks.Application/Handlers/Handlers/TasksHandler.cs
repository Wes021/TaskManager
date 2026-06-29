using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks.MembersModel;
using TaskManager.SharedLayer.RequestModels.Tasks.TasksModels;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Application.Handlers.IHandlers;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Application.Handlers.Handlers
{
    public class TasksHandler(ICurrentUserService _currentUserService, IStringLocalizer<SharedResource> _localizer,
        IFileManager _fileManager, ITasksService _tasksService
        ) : ITasksHandler
    {
        public async Task<ResponseModel<bool>> AddMembersToTask(AddMembersToCurrentTask model)
        {
            if (model.TaskId <= 0 || model.MembersModels.MemberIds.Count <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };

            if (_currentUserService.Role !=
                SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };


            var response = await _tasksService.AddMembersToTask(model);

            return response;
        }

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

        public async Task<ResponseModel<bool>> DeleteTask(int taskId, DeleteTask model)
        {
            if (model == null || taskId <= 0 || model.IsDeleted == false)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["InvalidRequest"]
                };

            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Employee.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };



            var response = await _tasksService.DeleteTask(taskId, model);

            return response;
        }

        public async Task<ResponseModel<TaskInfoDetails>> GetTaskById(int TaskId)
        {
            var currectUserRole = _currentUserService.Role;
            var currentUserId = _currentUserService.UserId;
            if (currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString() && currectUserRole != SystemEnums.UserType.Employee.ToString())
                return new ResponseModel<TaskInfoDetails>
                {
                    Success = false,

                    Message = _localizer["UserNotAllowed"]
                };

            if (TaskId <= 0)
                return new ResponseModel<TaskInfoDetails>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };

            var response = await _tasksService.GetTaskById(TaskId);


            return response;
        }

        public async Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByCurrentUserId(GetTasksRequest model)
        {
            var currectUserRole = _currentUserService.Role;
            var currentUserId = _currentUserService.UserId;
            if (currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString() && currectUserRole != SystemEnums.UserType.Employee.ToString())
                return new ResponseModel<PagedResult<TaskInfoDto>>
                {
                    Success = false,

                    Message = _localizer["UserNotAllowed"]
                };

            if (model == null)
                return new ResponseModel<PagedResult<TaskInfoDto>>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };


            var project = await _tasksService.GetTasksByUserId(model, currentUserId);

            return project;
        }

        public async Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByProjectID(GetTasksRequest model, int ProjectId)
        {
            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<PagedResult<TaskInfoDto>>
                {
                    Success = false,

                    Message = _localizer["UserNotAllowed"]
                };

            if (model == null)
                return new ResponseModel<PagedResult<TaskInfoDto>>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };


            var project = await _tasksService.GetTasksByProjectId(model, ProjectId);

            return project;
        }

        public async Task<ResponseModel<bool>> RemoveMembersFromTask(RemoveMembersFromCurrentTask model)
        {
            if (model.TaskId <= 0 || model.MembersModels.MemberIds.Count <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };

            if (_currentUserService.Role !=
                SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };


            var response = await _tasksService.RemoveMembersFromTask(model);

            return response;
        }

        public async Task<ResponseModel<bool>> SetTaskStatus(int taskId, UpdateTaskStatus model)
        {

            if (model == null || model.NewStatus <= 0 || taskId <= 0 || model.NewStatus <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["InvalidRequest"]
                };

            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Employee.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };




            var response = await _tasksService.SetTaskStatus(taskId, model);

            return response;
        }

        public async Task<ResponseModel<bool>> UpdateTask(int taskId, UpdateTaskInfo model)
        {
            if (model == null || string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Description) || taskId <= 0 || model.DueDate == null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["InvalidRequest"]
                };

            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Employee.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var response = await _tasksService.UpdateTask(taskId, model);

            return response;


        }
    }
}
