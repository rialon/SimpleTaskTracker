using SimpleTaskTracker.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskTracker.BLL.Dto;
using SimpleTaskTracker.BLL.Infrastructure;
using System.Security.Claims;
using SimpleTaskTracker.DAL.Interfaces;
using SimpleTaskTracker.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace SimpleTaskTracker.BLL.Services {

    public class UserService : IUserService {
        private ITaskUnitOfWork _tuow;

        public UserService(ITaskUnitOfWork tuow) {
            this._tuow = tuow;
        }

        public async Task<OperationDetails> Create(UserDto userDto) {
            var _user = await this._tuow.UserManager.FindByEmailAsync(userDto.Email);
            if (_user == null) {
                _user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var _result = await this._tuow.UserManager.CreateAsync(_user, userDto.Password);
                if (_result.Errors.Count() > 0) {
                    return new OperationDetails(false, _result.Errors.FirstOrDefault(), "");
                }
                await this._tuow.UserManager.AddToRoleAsync(_user.Id, userDto.Role);
                ClientProfile _clientProfile = new ClientProfile { Id = _user.Id, Name = userDto.Name, Surname = userDto.Surname };
                this._tuow.ClientManager.Create(_clientProfile);
                await this._tuow.SaveAsync();
                return new OperationDetails(true, "Successfull registration", "");
            } else {
                return new OperationDetails(false, "Email is not unique", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto) {
            ClaimsIdentity _claim = null;
            var _user = await this._tuow.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (_user != null) {
                _claim = await this._tuow.UserManager.CreateIdentityAsync(_user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return _claim;
        }

        public async Task SetInitialData(UserDto admin, List<string> roles) {
            foreach (var _roleName in roles) {
                var _role = await this._tuow.RoleManager.FindByNameAsync(_roleName);
                if (_role == null) {
                    await this._tuow.RoleManager.CreateAsync(new ApplicationRole { Name = _roleName });
                }
            }
            await this.Create(admin);
        }

        public string GetUserName(string id) {
            var _user = this._tuow.UserManager.FindById(id);
            if (_user == null) {
                throw new ValidationException("User is not found", "");
            }
            return _user.UserName;
        }

        public void Dispose() {
            this._tuow.Dispose();
        }
    }
}
