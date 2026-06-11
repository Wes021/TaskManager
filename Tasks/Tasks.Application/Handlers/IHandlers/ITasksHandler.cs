using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;

namespace Tasks.Tasks.Application.Handlers.IHandlers
{
    public interface ITasksHandler
    {
        Task<ResponseModel<bool>> AddNewTask(AddNewTaksDTO model);
    }
}
