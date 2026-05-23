using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.Enums
{
    public class SystemEnums
    {
        public enum UserType 
        {
            Employee = 1, 
            Admin = 2,
            ManagerAndLeader = 3
            
        }


        public enum ProjectStatus
        {
            Draft = 1,
            Active = 2,
            OnHold = 3,
            Completed = 4,
            Cancelled = 5
        }
    }
}
