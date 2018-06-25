using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SimpleTaskTracker.BLL.Dto;
using SimpleTaskTracker.BLL.Interfaces;
using SimpleTaskTracker.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleTaskTracker.WEB.Controllers {

    public class AdminController : Controller {

        private ITaskService _taskService;
        private IUserService _UserService {
            get {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public AdminController(ITaskService taskService) {
            this._taskService = taskService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index() {
            var _userTaskDtos = this._taskService.GetAll();
            var _userTasks = new List<AdminUserTaskViewModel>();
            foreach (var _userTaskDto in _userTaskDtos) {
                _userTasks.Add(new AdminUserTaskViewModel {
                    Id = _userTaskDto.Id,
                    Name = _userTaskDto.Name,
                    Description = _userTaskDto.Description,
                    DueDate = _userTaskDto.DueDate,
                    UserName = this._UserService.GetUserName(_userTaskDto.ApplicationUserId)
                });
            }
            return View(_userTasks);
        }
    }
}