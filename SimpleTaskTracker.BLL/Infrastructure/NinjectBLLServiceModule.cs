using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using SimpleTaskTracker.DAL.Interfaces;
using SimpleTaskTracker.DAL.Repositories;

namespace SimpleTaskTracker.BLL.Infrastructure {

    public class NinjectBLLServiceModule : NinjectModule {
        private string _connectionString;

        public NinjectBLLServiceModule(string connectionString) {
            this._connectionString = connectionString;
        }

        public override void Load() {
            this.Bind<ITaskUnitOfWork>().To<TaskUnitOfWork>().WithConstructorArgument(this._connectionString);
        }
    }
}
