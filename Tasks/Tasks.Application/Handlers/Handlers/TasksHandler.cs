using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Application.Handlers.IHandlers;

namespace Tasks.Tasks.Application.Handlers.Handlers
{
    public class TasksHandler : ITasksHandler
    {
        public Task<ResponseModel<bool>> AddNewTask(AddNewTaksDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
