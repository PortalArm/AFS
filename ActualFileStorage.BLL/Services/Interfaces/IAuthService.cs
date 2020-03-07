﻿using ActualFileStorage.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        UserDTO Auth(string input, string password);
    }
}
