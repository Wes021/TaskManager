using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Domain.IRepositories
{
    public interface IProjectMemberRepository
    {

        Task<ProjectMember> Add(ProjectMember entity);

        Task<List<ProjectMember>> GetProjectByProjectIdAsync(
             int Id,
             Func<IQueryable<ProjectMember>, IQueryable<ProjectMember>>? include = null,
             bool isTracked = true);


        Task<ProjectMember> GetProjectByMemberIdAsync(
             int Id,
             Func<IQueryable<ProjectMember>, IQueryable<ProjectMember>>? include = null,
             bool isTracked = true);


        Task<List<ProjectMember>> GetAssignedUserIdsAsync(List<int> userIds);




    }
}
