using SimpleTaskTracker.DAL.Entities;
using SimpleTaskTracker.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskTracker.DAL.Interfaces {

    public interface ITaskUnitOfWork : IDisposable {
        ApplicationUserManager UserManager { get;}
        ApplicationRoleManager RoleManager { get;}
        IClientManager ClientManager { get; }
        IRepository<UserTask> Tasks { get; }

        Task SaveAsync();
    }
}
