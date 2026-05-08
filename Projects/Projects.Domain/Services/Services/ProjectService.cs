using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Projects.Projects.Domain.IRepositories;
using Projects.Projects.Domain.IUnitOfWork;
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
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Domain.Services.Services
{
    public class ProjectService(IProjectsRepository _projectsRepository, IStringLocalizer<SharedResource> _localizer
        , IProjectStatusRepository _projectStatusRepository, ICurrentUserService _currentUserService,
        IProjectModuleUoW _projectModuleUoW, IMapper _mapper, IUserLookupService _userLookupService) : IProjectService
    {
        public async Task<ResponseModel<bool>> AddProject(CreateProjectDto model)
        {
            var projectExists = await _projectsRepository.CheckProjectExists(model);

            if (projectExists)
                return new ResponseModel<bool> { Success = false, Message = _localizer["ProjectNameAlreadyuExists"] };

            var statusExists = await _projectStatusRepository.CheckProjectStatusExists(model.StatusId);

            if (!statusExists)
                return new ResponseModel<bool> { Success = false, Message = _localizer["StatusNotExists"] };


            var userStatus = await _userLookupService.GetUserByIdAsync(_currentUserService.UserId);
            if (!userStatus.IsActive || userStatus.IsDeleted)
                return new ResponseModel<bool> { Success = false, Message = _localizer["UserNotActiveOrDeletedCantAdd"] };





            if (_currentUserService.Role.ToString() != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };


            var newProject = Project.Create(model.Name, model.Description, model.StartDate, model.EndDate, _currentUserService.UserId, model.StatusId, _currentUserService.UserId);

            if (!newProject.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = newProject.Error
                };


            await _projectsRepository.Add(newProject.Data);
            await _projectModuleUoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["ProjectAddedSuccessfully"]
            };
        }

        public async Task<ResponseModel<bool>> DeleteProject(int ProjectId, UpdateProjectStatus model)
        {

            var project = await _projectsRepository.GetProjectByIdAsync(ProjectId);
            var currentUserId = _currentUserService.UserId;
            var currectUserRole = _currentUserService.Role;

            if (project is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["ProjectDoesNotExist"]
                };

            if (currectUserRole != SystemEnums.UserType.Admin.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = project.SetIsDeleted(model.IsDeleted, currentUserId);


            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };

            await _projectModuleUoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["ProjectDeletedSuccessfully"]
            };
        }

        public async Task<ResponseModel<ProjectInfoDto>> GetProjectById(int id)
        {

            


            var project = await _projectsRepository.GetProjectByIdAsync(
                id,
                x => x.Include(x => x.Status),
                false);

            if (project == null)
            {
                return new ResponseModel<ProjectInfoDto>
                {
                    Success = false,
                    Message = _localizer["ProjectNotFound"]
                };
            }

            var managerTask = await
                _userLookupService.GetUserByIdAsync(project.ManagerId);

            UserLookupDto? creatorTask = null;

            if (project.CreatedUser.HasValue)
            {
                creatorTask = await
                    _userLookupService.GetUserByIdAsync(project.CreatedUser.Value);
            }

            

            var mappedData = _mapper.Map<ProjectInfoDto>(project);

            mappedData.Manager = managerTask?.FullName;

            if (creatorTask != null)
            {
                mappedData.CreatedUser =
                     creatorTask?.FullName;
            }

            return new ResponseModel<ProjectInfoDto>
            {
                Success = true,
                Data = mappedData,
                Message = _localizer["DataReturnedSuccessfully"]
            };
        }

        public async Task<ResponseModel<PagedResult<ProjectInfoDto>>> GetProjects(GetProjectsRequest model)
        {

            if (_currentUserService.Role != SystemEnums.UserType.ManagerAndLeader.ToString() || _currentUserService.Role != SystemEnums.UserType.Admin.ToString())
                return new ResponseModel<PagedResult<ProjectInfoDto>>
                {
                    Success = false,

                    Message = _localizer["UserNotAllowed"]
                };


            var projects = await _projectsRepository.GetProjectsAsync(model);

            var managerIds = projects.Items
      .Select(x => x.ManagerId)
      .Distinct()
      .ToList();


            var managers = await _userLookupService
    .GetUsersByIdsAsync(managerIds);

            var managersDictionary = managers
    .ToDictionary(x => x.Id);



            var CreatedUsersIds = projects.Items.Select(x => x.CreatedUserId).Distinct().ToList();

            var createdUsers = await _userLookupService
    .GetUsersByIdsAsync(CreatedUsersIds);

            var CreatedUsersDictionary = createdUsers.ToDictionary(x => x.Id);




            foreach (var project in projects.Items)
            {
                if (managersDictionary.TryGetValue(project.ManagerId, out var manager))
                {
                    project.Manager = manager.FullName;
                }
            }


            foreach (var project in projects.Items)
            {
                if (CreatedUsersDictionary.TryGetValue(project.CreatedUserId, out var manager))
                {
                    project.CreatedUser = manager.FullName;
                }
            }


            return new ResponseModel<PagedResult<ProjectInfoDto>> { Success = true, Data = projects, Message = _localizer["DataRetunedSuccssefully"] };

        }

        public async Task<ResponseModel<bool>> UpdateProjectStatus(int ProjectId, UpdateProjectStatus model)
        {
            var project = await _projectsRepository.GetProjectByIdAsync(ProjectId);
            var currentUserId = _currentUserService.UserId;
            var currectUserRole = _currentUserService.Role;

            if (project is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["ProjectDoesNotExist"]
                };

            if (currectUserRole != SystemEnums.UserType.Admin.ToString() && currectUserRole != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = project.SetIsActive(model.IsActive, currentUserId);


            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };

            await _projectModuleUoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["ProjectUpdatedSuccessfully"]
            };
        }
    }
}
