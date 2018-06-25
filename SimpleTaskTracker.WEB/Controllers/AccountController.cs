using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SimpleTaskTracker.BLL.Interfaces;
using SimpleTaskTracker.WEB.Models;
using SimpleTaskTracker.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SimpleTaskTracker.WEB.Controllers {

    public class AccountController : Controller {
        private IUserService _UserService {
            get {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager _Authentication {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel) {
            await this._SetInitialData();
            if (ModelState.IsValid) {
                var _userDto = new UserDto { Email = loginModel.Email, Password = loginModel.Password };
                var _claim = await this._UserService.Authenticate(_userDto);
                if (_claim != null) {
                    this._Authentication.SignOut();
                    this._Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, _claim);
                    return RedirectToAction("Index", "Home");
                } else {
                    ModelState.AddModelError("", "Invalid login or password");
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout() {
            this._Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel registerModel) {
            await this._SetInitialData();
            if (ModelState.IsValid) {
                var _userDto = new UserDto {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    Name = registerModel.Name,
                    Surname = registerModel.Surname,
                    Role = "User"
                };
                var _result = await this._UserService.Create(_userDto);
                if (_result.Succeeded) {
                    return View("SuccessRegister");
                } else {
                    ModelState.AddModelError(_result.Property, _result.Message);
                }
            } else {
                var _duplicatedModelState = ModelState.Values.SingleOrDefault(ms => ms.Errors.Count > 1 && ms.Errors[0].ErrorMessage == ms.Errors[1].ErrorMessage);
                if (_duplicatedModelState != null) {
                    _duplicatedModelState.Errors.Remove(_duplicatedModelState.Errors[1]);
                }
            }
            return View(registerModel);
        }

        private async Task _SetInitialData() {
            await this._UserService.SetInitialData(new UserDto {
                Email = "test@admin",
                UserName = "test@admin",
                Password = "adminTest",
                Name = "Alex",
                Surname = "Baker",
                Role = "Admin",
            }, new List<string> { "User", "Admin" });
        }
    }
}