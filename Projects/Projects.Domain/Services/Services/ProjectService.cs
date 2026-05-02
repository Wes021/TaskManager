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
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;

namespace Projects.Projects.Domain.Services.Services
{
    public class ProjectService(IProjectsRepository _projectsRepository, IStringLocalizer<SharedResource> _localizer
        ,IProjectStatusRepository _projectStatusRepository, ICurrentUserService _currentUserService, IProjectModuleUoW _projectModuleUoW) : IProjectService
    {
        public async Task<ResponseModel<bool>> AddProject(CreateProjectDto model)
        {
            var projectExists = await _projectsRepository.CheckProjectExists(model);

            if (projectExists)
                return new ResponseModel<bool> { Success = false, Message = _localizer["ProjectNameAlreadyuExists"] };

            var statusExists = await _projectStatusRepository.CheckProjectStatusExists(model.StatusId);

            if (statusExists)
                return new ResponseModel<bool> { Success = false, Message = _localizer["StatusNotExists"] };

            if (_currentUserService.Roles.ToString() != SystemEnums.UserType.ManagerAndLeader.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };


            var newProject = Project.Create(model.Name, model.Description, model.StartDate, model.EndDate, model.ManagerId, model.StatusId, _currentUserService.UserId);


            await _projectsRepository.Add(newProject.Data);
            await _projectModuleUoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["ProjectAddedSuccessfully"]
            };
        }
    }
}
