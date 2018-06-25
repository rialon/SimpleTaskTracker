using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleTaskTracker.WEB.Models {

    public class AdminUserTaskViewModel : UserTaskViewModel {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}