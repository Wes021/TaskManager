using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Projects;

namespace Projects.Projects.Domain.IRepositories
{
    public interface IProjectsRepository
    {
        Task<bool> CheckProjectExists(CreateProjectDto entity);

        Task<Project> Add(Project entity);
    }
}
