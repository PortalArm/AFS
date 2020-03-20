﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Hashers
{
    public interface IHash
    {
        string Hash(byte[] bytes);
        string Hash(string str);
    }
}
