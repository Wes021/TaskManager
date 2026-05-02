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
            ManagerAndLeader = 7 

        }


        public enum ProjectStatus
        {
            Draft = 0,
            Active = 1,
            OnHold = 2,
            Completed = 3,
            Cancelled = 4
        }
    }
}
