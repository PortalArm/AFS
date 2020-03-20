﻿using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserDTO> GetUsers();
        //void UpdateRoles(int userId, Dictionary<Role, bool> roles);
        void UpdateRoles(IEnumerable<ChangeRoleDTO> roles);
    }
}
