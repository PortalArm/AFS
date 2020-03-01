using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Models
{
    public class RegistrationUserViewModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9]{2,}")]
        [Remote("IsLoginPresent", "Register")]
        public string Login { get; set; } // req 
        [Required]
        [RegularExpression(@"\w+", ErrorMessage = "Enter your first name")]
        public string FirstName { get; set; } // req 
        [RegularExpression(@"\w+", ErrorMessage = "Enter valid second name")]
        public string SecondName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        [RegularExpression(@"\w+@\w+\.\w+", ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //public string Salt { get; set; }
        //public string PassHash { get; set; } //  => Hash(Operation(Password, Salt))

    }

}