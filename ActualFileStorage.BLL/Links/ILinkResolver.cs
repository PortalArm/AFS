﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Links
{
    public interface ILinkResolver
    {
        string Encode(string fullString);
        string Decode(string shortString);
    }
}
