using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace SimpleTaskTracker.WEB.Models {

    public class UserTaskViewModel : IValidatableObject {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name of the task")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Description of the task")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Due Date of the task")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext) {
            var _result = new List<ValidationResult>();
            if (this.DueDate <= DateTime.Today) {
                var _valError = new ValidationResult("Due Date must be greater than today date");
                _result.Add(_valError);
            }
            return _result;
        }
    }
}