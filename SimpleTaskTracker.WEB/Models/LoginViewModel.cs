﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleTaskTracker.WEB.Models {

    public class LoginViewModel {
        [Required(ErrorMessage ="Please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}