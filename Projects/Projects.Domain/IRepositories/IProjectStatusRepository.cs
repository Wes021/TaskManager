using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Projects.Domain.IRepositories
{
    public interface IProjectStatusRepository
    {
        Task<bool> CheckProjectStatusExists(int StatusId);
    }
}
