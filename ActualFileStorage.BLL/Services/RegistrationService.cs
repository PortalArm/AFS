using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    class RegistrationService // : IRoleSaltGen, IRolePassHashGen
    {
        private ISaltResolver _saltGen;
        private IPasswordHasher _passHasher;
        public RegistrationService(ISaltResolver saltGen, IPasswordHasher passHasher)
        {
            _saltGen = saltGen;
            _passHasher = passHasher;
        }
        public string GenerateSalt(int size) => _saltGen.GetSalt(size);
        public string GenerateHash(string pass, string salt) => _passHasher.HashPass(pass, salt);
    }
}
