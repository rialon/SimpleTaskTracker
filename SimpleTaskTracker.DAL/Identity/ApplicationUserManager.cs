﻿using Microsoft.AspNet.Identity;
using SimpleTaskTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskTracker.DAL.Identity {

    public class ApplicationUserManager : UserManager<ApplicationUser> {

        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) {
        }
    }
}
