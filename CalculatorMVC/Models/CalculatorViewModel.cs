using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorMVC.Models
{
    public class CalculatorViewModel
    {
        [Display(Name = "String to be calculated")]
        [Required(ErrorMessage = "You must enter a string that you want calculated")]
        public string UserInput { get; set; }

        [Display(Name = "Calculated value from string")]
        public string CalculatedValue { get; set; }
    }
}
