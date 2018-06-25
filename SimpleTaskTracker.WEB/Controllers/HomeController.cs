using SimpleTaskTracker.BLL.Interfaces;
using SimpleTaskTracker.BLL.Dto;
using SimpleTaskTracker.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using SimpleTaskTracker.BLL.Infrastructure;
using System.Threading.Tasks;

namespace SimpleTaskTracker.WEB.Controllers {

    public class HomeController : Controller {
        private ITaskService _taskService;

        public HomeController(ITaskService taskService) {
            this._taskService = taskService;
        }

        [Authorize]
        public ActionResult Index() {
            var _userTaskDtos = this._taskService.GetForUser(User.Identity.GetUserId());
            var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserTaskDto, UserTaskViewModel>()).CreateMapper();
            var _userTasks = _mapper.Map<IEnumerable<UserTaskDto>, List<UserTaskViewModel>>(_userTaskDtos);
            return View(_userTasks);
        }

        [Authorize]
        public ActionResult Create() {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(UserTaskViewModel taskModel) {
            if (ModelState.IsValid) {
                var _userTaskDto = new UserTaskDto {
                    Name = taskModel.Name,
                    Description = taskModel.Description,
                    DueDate = taskModel.DueDate,
                    ApplicationUserId = User.Identity.GetUserId()
                };
                try {
                    await this._taskService.Create(_userTaskDto);
                    return RedirectToAction("Index", "Home");//Add partial "New task was created!"
                } catch (ValidationException ex) {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
            }
            return View(taskModel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Remove(int taskId) {
            try {
                this._taskService.Remove(taskId);
            } catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            if (User.IsInRole("Admin")) {
                return RedirectToAction("Index", "Admin");
            } else {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}