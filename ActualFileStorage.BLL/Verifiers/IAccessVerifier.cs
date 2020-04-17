using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Verifiers
{
    public interface IAccessVerifier
    {
        bool IsAccessibleByVisibility(int? caller, int target, FileAccess vis);
    }
}
