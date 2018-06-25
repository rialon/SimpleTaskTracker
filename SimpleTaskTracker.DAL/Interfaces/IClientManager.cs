using SimpleTaskTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskTracker.DAL.Interfaces {

    public interface IClientManager : IRepository<ClientProfile>, IDisposable {
    }
}
