using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Verifiers
{
    public class AccessVerifier : IAccessVerifier
    {
        public bool IsAccessibleByVisibility(int? caller, int target, FileAccess vis)
        {
            switch (vis)
            {
                case FileAccess.Private:
                    return target == caller;
                case FileAccess.Public:
                    return true;
                case FileAccess.RegisteredUsers:
                    return caller.HasValue;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
