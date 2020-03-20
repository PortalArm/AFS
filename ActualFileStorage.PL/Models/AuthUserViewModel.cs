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
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "AuthLoginValue")]
        public string Value { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display( ResourceType = typeof(Localization.InfoRes), Name = "AuthPassword")]
        public string Password { get; set; }

    }
}