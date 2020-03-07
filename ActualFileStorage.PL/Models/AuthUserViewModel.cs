using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class AuthUserViewModel
    {
        [Required]
        public string Value { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}