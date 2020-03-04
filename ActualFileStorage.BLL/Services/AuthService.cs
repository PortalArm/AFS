using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _uow;
        private IPasswordHasher _hasher;
        private ISaltResolver _salter;
        public AuthService(IUnitOfWork uow, IPasswordHasher hasher, ISaltResolver salter)
        {
            _uow = uow; _hasher = hasher; _salter = salter;

        }
        public bool Auth(string input, string password)
        {
            var users = _uow.GetRepo<User>();
            var possibleUsers = users.GetByPredicate(u => u.Login == input);
            if(possibleUsers == null || !possibleUsers.Any())
                return false;
            var user = possibleUsers.First();
            return user.PassHash == _hasher.HashPass(password, user.Salt);
        }
    }
}
