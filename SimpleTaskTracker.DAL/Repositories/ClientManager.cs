using SimpleTaskTracker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskTracker.DAL.Entities;
using SimpleTaskTracker.DAL.EF;
using System.Data.Entity;

namespace SimpleTaskTracker.DAL.Repositories {

    public class ClientManager : IClientManager {
        private ApplicationTaskContext _db;

        public ClientManager(ApplicationTaskContext dbContext) {
            this._db = dbContext;
        }

        public void Create(ClientProfile item) {
            this._db.ClientProfiles.Add(item);
        }

        public void Update(ClientProfile item) {
            this._db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(ClientProfile item) {
            this._db.ClientProfiles.Remove(item);
        }

        public ClientProfile Get(int id) {
            return this._db.ClientProfiles.Find(id);
        }

        public IEnumerable<ClientProfile> GetAll() {
            return this._db.ClientProfiles;
        }

        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate) {
            return this._db.ClientProfiles.Where(predicate).ToList();
        }

        public void Dispose() {
            this._db.Dispose();
        }
    }
}
