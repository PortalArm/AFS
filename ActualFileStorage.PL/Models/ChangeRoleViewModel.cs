using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class ChangeRoleViewModel
    {
        public int Id { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
    public class RoleViewModel
    {
        public string Role { get; set; }
        public bool Enabled { get; set; }
    }
}