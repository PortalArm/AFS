using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class ChangeRoleDTO
    {
        public int Id { get; set; }
        public Dictionary<Role, bool> Roles { get; set; }
    }
}
