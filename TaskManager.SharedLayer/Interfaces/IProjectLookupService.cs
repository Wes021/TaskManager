using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IProjectLookupService
    {
        Task<ResponseModel<ProjectInfoDto>> GetProjectById(int Id);
    }
}
