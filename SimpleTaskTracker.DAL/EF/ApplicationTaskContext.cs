using Microsoft.AspNet.Identity.EntityFramework;
using SimpleTaskTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SimpleTaskTracker.DAL.EF {

    public class ApplicationTaskContext : IdentityDbContext<ApplicationUser> {

        public ApplicationTaskContext(string connectionString) : base(connectionString) {
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
    }
}
