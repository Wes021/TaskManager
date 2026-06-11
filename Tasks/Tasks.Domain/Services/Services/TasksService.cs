using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TasksService(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer, IProjectLookupService projectLookupService) : ITasksService
    {
        public async Task<ResponseModel<bool>> AddNewTask(AddNewTaksDTO model, AddMembersToTask MembersModel)
        {
            var projectInfo =await projectLookupService.GetProjectById(model.ProjectId);

            
            if (projectInfo == null || projectInfo.Data.IsDeleted == true || projectInfo.Data.IsActive == false)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["ProjectNotFoundOrDeactivated"]
                };



            

            throw new NotImplementedException();
        }
    }
}
