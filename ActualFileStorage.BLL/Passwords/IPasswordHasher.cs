using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Passwords
{
    public interface IPasswordHasher
    {
        string HashPass(string password, string salt);
    }
}
