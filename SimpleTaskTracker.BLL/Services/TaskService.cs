using SimpleTaskTracker.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskTracker.BLL.Dto;
using SimpleTaskTracker.DAL.Interfaces;
using SimpleTaskTracker.DAL.Entities;
using SimpleTaskTracker.BLL.Infrastructure;
using AutoMapper;

namespace SimpleTaskTracker.BLL.Services {

    public class TaskService : ITaskService {
        private ITaskUnitOfWork _tuow;

        public TaskService(ITaskUnitOfWork tuow) {
            this._tuow = tuow;
        }

        public async Task Create(UserTaskDto item) {
            var _user = await this._tuow.UserManager.FindByIdAsync(item.ApplicationUserId);
            if (_user == null) {
                throw new ValidationException("User is not found", "");
            }
            var _task = new UserTask {
                Name = item.Name,
                Description = item.Description,
                CreateDate = DateTime.Now,
                DueDate = item.DueDate,
                ApplicationUserId = _user.Id
            };
            this._tuow.Tasks.Create(_task);
            await this._tuow.SaveAsync();
        }

        public void Remove(int id) {
            var _task = this._tuow.Tasks.Get(id);
            if (_task == null) {
                throw new ValidationException("User Task is not found", "");
            }
            this._tuow.Tasks.Remove(_task);
            this._tuow.SaveAsync();
        }

        public IEnumerable<UserTaskDto> GetAll() {
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserTask, UserTaskDto>()).CreateMapper();
            return _mapper.Map<IEnumerable<UserTask>, List<UserTaskDto>>(this._tuow.Tasks.GetAll());
        }

        public IEnumerable<UserTaskDto> GetForUser(string userId) {
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserTask, UserTaskDto>()).CreateMapper();
            return _mapper.Map<IEnumerable<UserTask>, List<UserTaskDto>>(this._tuow.Tasks.Find(t => t.ApplicationUserId == userId));
        }

        public void Dispose() {
            this._tuow.Dispose();
        }
    }
}
