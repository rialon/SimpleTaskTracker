using SimpleTaskTracker.DAL.EF;
using SimpleTaskTracker.DAL.Entities;
using SimpleTaskTracker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SimpleTaskTracker.DAL.Repositories {

    public class TaskRepository : IRepository<UserTask> {
        private ApplicationTaskContext _db;

        public TaskRepository(ApplicationTaskContext dbContext) {
            this._db = dbContext;
        }

        public void Create(UserTask item) {
            this._db.Tasks.Add(item);
        }

        public void Update(UserTask item) {
            this._db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(UserTask item) {
            this._db.Tasks.Remove(item);
        }

        public UserTask Get(int id) {
            return this._db.Tasks.Find(id);
        }

        public IEnumerable<UserTask> GetAll() {
            return this._db.Tasks.Include(t => t.ApplicationUser);
        }


        public IEnumerable<UserTask> Find(Func<UserTask, bool> predicate) {
            return this._db.Tasks.Include(t => t.ApplicationUser).Where(predicate).ToList();
        }

        public void Dispose() {
            this._db.Dispose();
        }
    }
}
