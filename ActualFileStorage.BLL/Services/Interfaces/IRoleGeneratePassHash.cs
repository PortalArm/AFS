﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IRoleGeneratePassHash
    {
        string GenerateHash(string pass, string hash);
    }
}
