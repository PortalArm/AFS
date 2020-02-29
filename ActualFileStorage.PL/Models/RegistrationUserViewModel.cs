using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class RegistrationUserViewModel
    {
        //public int Id { get; set; } // req 
        [Required]
        public string Login { get; set; } // req 
        [Required]
        [RegularExpression(@"\w{1,}")]
        public string FirstName { get; set; } // req 
        [RegularExpression(@"\w{1,}")]
        public string SecondName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        //[RegularExpression(@"\w{,20}@\w{3,10}.\w{3,10}")]
        public string Email { get; set; }
        //public string Salt { get; set; }
        //public string PassHash { get; set; } //  => Hash(Operation(Password, Salt))

    }
}