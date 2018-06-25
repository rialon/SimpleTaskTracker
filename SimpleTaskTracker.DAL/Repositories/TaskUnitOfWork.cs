using SimpleTaskTracker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskTracker.DAL.Entities;
using SimpleTaskTracker.DAL.Identity;
using SimpleTaskTracker.DAL.EF;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SimpleTaskTracker.DAL.Repositories {

    public class TaskUnitOfWork : ITaskUnitOfWork {
        private ApplicationTaskContext _db;
        private IClientManager _clientManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private TaskRepository _tasksRepository;

        private bool _disposed = false;

        public TaskUnitOfWork(string connectionString) {
            this._db = new ApplicationTaskContext(connectionString);
            this._clientManager = new ClientManager(this._db);
            this._userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(this._db));
            this._roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(this._db));
            this._tasksRepository = new TaskRepository(this._db);
        }


        public IClientManager ClientManager {
            get {
                return this._clientManager;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return this._userManager;
            }
        }

        public ApplicationRoleManager RoleManager {
            get {
                return this._roleManager;
            }
        }

        public IRepository<UserTask> Tasks {
            get {
                return this._tasksRepository;
            }
        }

        public async Task SaveAsync() {
            await this._db.SaveChangesAsync();
        }

        private void _Dispose(bool disposing) {
            if (!this._disposed) {
                if (disposing) {
                    this._db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose() {
            this._Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
