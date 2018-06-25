using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using SimpleTaskTracker.BLL.Interfaces;
using SimpleTaskTracker.BLL.Services;

namespace SimpleTaskTracker.WEB.Infrastructure {

    public class NinjectWEBServiceModule : NinjectModule {

        public override void Load() {
            this.Bind<IUserService>().To<UserService>();
            this.Bind<ITaskService>().To<TaskService>();
        }
    }
}