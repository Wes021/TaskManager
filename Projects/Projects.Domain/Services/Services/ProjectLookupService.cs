using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Projects.Projects.Domain.IRepositories;
using Projects.Projects.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Domain.Services.Services
{
    public class ProjectLookupService(IProjectsRepository _projectsRepository, IStringLocalizer<SharedResource> _localizer
        , IProjectStatusRepository _projectStatusRepository, ICurrentUserService _currentUserService,
        IProjectModuleUoW _projectModuleUoW, IMapper _mapper, IUserLookupService _userLookupService,
        IProjectMemberRepository _projectMemberRepository) : IProjectLookupService
    {

        public async Task<ResponseModel<ProjectInfoDto>> GetProjectById(int id)
        {

            var project = await _projectsRepository.GetProjectByIdAsync(
                id,
                x => x.Include(x => x.Status).Include(m => m.Members.Where(n => !n.IsDeleted && n.IsActive)),
                false);

            if (project == null )
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


            var MembersInfo = await _userLookupService.GetUsersByIdsAsync(project.Members.Select(x => x.UserId).ToList());
            var mappedData = _mapper.Map<ProjectInfoDto>(project);
            mappedData.ProjectMembers = _mapper.Map<List<ProjectMembersDto>>(MembersInfo);

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
    }
}
