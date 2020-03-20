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
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "RegLoginValue")]
        public string Login { get; set; } // req 
        [Required]
        [RegularExpression(@"\w+", ErrorMessageResourceType = typeof(Localization.InfoRes), ErrorMessageResourceName = "RegErrorFirstName")]
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "RegFirstName")]
        public string FirstName { get; set; } // req 
        [RegularExpression(@"\w+", ErrorMessage = "Enter valid second name")]
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "RegSecondName")]
        public string SecondName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "RegBDate")]
        public DateTime BirthDate { get; set; }
        [Required]
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "RegEmail")]
        [RegularExpression(@"\w+@\w+\.\w+", ErrorMessageResourceType = typeof(Localization.InfoRes), ErrorMessageResourceName = "RegErrorEmail")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "RegPassword")]
        public string Password { get; set; }
        //public string Salt { get; set; }
        //public string PassHash { get; set; } //  => Hash(Operation(Password, Salt))

    }

}