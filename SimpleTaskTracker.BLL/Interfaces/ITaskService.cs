using SimpleTaskTracker.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskTracker.BLL.Interfaces {

    public interface ITaskService : IDisposable {
        Task Create(UserTaskDto item);
        void Remove(int id);
        IEnumerable<UserTaskDto> GetAll();
        IEnumerable<UserTaskDto> GetForUser(string userId);
    }
}
