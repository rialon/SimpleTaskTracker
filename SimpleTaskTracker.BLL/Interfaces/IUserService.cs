using SimpleTaskTracker.BLL.Dto;
using SimpleTaskTracker.BLL.Infrastructure;
using SimpleTaskTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskTracker.BLL.Interfaces {

    public interface IUserService : IDisposable {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(UserDto admin, List<string> roles);
        string GetUserName(string id);
    }
}
