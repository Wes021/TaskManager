using Microsoft.Extensions.Localization;
using Projects.Projects.Application.Handlers.IHandler;
using Projects.Projects.Domain.Models;
using Projects.Projects.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Application.Handlers.Handler
{
    public class ProjectHandler(IProjectService _projectService, IStringLocalizer<SharedResource> _localizer, ICurrentUserService _currentUserService) : IProjectHandler
    {
        public async Task<ResponseModel<bool>> AddProjectAsync(CreateProjectDto model)
        {
            if (model == null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
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


            model.Name = model.Name?.Trim();

            model.Description = model.Description?.Trim();

            var result = await _projectService.AddProject(model);
            return result;
        }

        public async Task<ResponseModel<bool>> DeleteProjectAsync(int Id, UpdateProjectStatus model)
        {
            var currectUserRole = _currentUserService.Role;

            if (model == null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["InvalidRequest"]
                };


            if (currectUserRole != SystemEnums.UserType.Admin.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = await _projectService.DeleteProject(Id, model);

            return result;
        }

        public async Task<ResponseModel<ProjectInfoDto>> GetProjectByIdAsync(int Id)
        {
            if (Id <= 0)
                return new ResponseModel<ProjectInfoDto>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };


            var project = await _projectService.GetProjectById(Id);

            return project;

        }



        public async Task<ResponseModel<PagedResult<ProjectInfoDto>>> GetProjectsAsync(GetProjectsRequest model)
        {
            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString() && currectUserRole != SystemEnums.UserType.Admin.ToString())
                return new ResponseModel<PagedResult<ProjectInfoDto>>
                {
                    Success = false,

                    Message = _localizer["UserNotAllowed"]
                };

            if (model == null)
                return new ResponseModel<PagedResult<ProjectInfoDto>>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };


            var project = await _projectService.GetProjects(model);

            return project;
        }




        public async Task<ResponseModel<bool>> UpdateProjectStatusAsync(int Id, UpdateProjectStatus model)
        {

            if (model == null || Id <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["InvalidRequest"]
                };


            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Admin.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = await _projectService.UpdateProjectStatus(Id, model);

            return result;
        }


        public async Task<ResponseModel<bool>> UpdateProjectAsync(int Id, UpdateProjectInfo model)
        {

            if (model == null || Id <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["InvalidRequest"]
                };

            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Admin.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = await _projectService.UpdateProjectInfo(Id, model);

            return result;
        }



        public async Task<ResponseModel<bool>> AddMembersToProject(int ProjectId, AddProjectMembersDto model)
        {
            var currentUserId = _currentUserService.UserId;
            var currectUserRole = _currentUserService.Role;
            if (ProjectId <= 0 || model?.MemberIds == null || !model.MemberIds.Any())
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["SomthingWentWrong"]
                };


            model.MemberIds = model.MemberIds.Distinct().ToList();

            if (currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = await _projectService.AddMembersToProject(ProjectId, model);

            return result;
        }

        public async Task<ResponseModel<bool>> RemoveMembersFromProject(int ProjectId, RemoveProjectMembersDto model)
        {
            var currentUserId = _currentUserService.UserId;
            var currectUserRole = _currentUserService.Role;
            if (ProjectId <= 0 || model?.MemberIds == null || !model.MemberIds.Any())
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["SomthingWentWrong"]
                };


            model.MemberIds = model.MemberIds.Distinct().ToList();

            if (currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = await _projectService.RemoveMembersFromProject(ProjectId, model);

            return result;
        }
    }
}
