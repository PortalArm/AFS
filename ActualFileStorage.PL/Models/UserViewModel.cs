using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; } 
        public string Login { get; set; }
        public string FirstName { get; set; } 
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public FileAccess RootFolderAccess { get; set; }
        public IEnumerable<string> Roles { get; set; }

    }
}