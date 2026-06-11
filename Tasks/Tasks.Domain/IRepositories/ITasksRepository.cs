using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Tasks.Domain.Models;

namespace Tasks.Tasks.Domain.IRepositories
{
    public interface ITasksRepository
    {
        Task<Tasks.Domain.Models.Tasks> Add(Tasks.Domain.Models.Tasks entity);
    }
}
