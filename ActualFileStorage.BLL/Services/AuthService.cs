using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.UOW;
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
        private IUnitOfWork _uow;
        private IPasswordHasher _hasher;
        private ISaltResolver _salter;
        private IMapper _mapper;
        public AuthService(IUnitOfWork uow, IPasswordHasher hasher, ISaltResolver salter, IMapper mapper)
        {
            _uow = uow; _hasher = hasher; _salter = salter; _mapper = mapper;

        }
        public UserDTO Auth(string input, string password)
        {
            var users = _uow.GetRepo<User>();
            var possibleUsers = users.GetByPredicate(u => u.Login == input);
            if(possibleUsers == null || !possibleUsers.Any())
                return null;
            var user = possibleUsers.First();
            return user.PassHash == _hasher.HashPass(password, user.Salt) ? _mapper.Map<UserDTO>(user) : null;
        }
    }
}
