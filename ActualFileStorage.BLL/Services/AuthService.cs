using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _users;
        private IPasswordHasher _hasher;
        private ISaltBuilder _salter;
        private IMapper _mapper;
        public AuthService(IUserRepository users, IPasswordHasher hasher, ISaltBuilder salter, IMapper mapper)
        {
            _users = users; 
            _hasher = hasher;
            _salter = salter;
            _mapper = mapper;

        }
        public UserDTO Auth(string input, string password)
        {
            var possibleUsers = _users.GetByPredicate(u => u.Login == input);
            if(possibleUsers == null || !possibleUsers.Any())
                return null;
            var user = possibleUsers.First();
            return user.PassHash == _hasher.HashPass(password, user.Salt) ? _mapper.Map<UserDTO>(user) : null;
        }
    }
}
