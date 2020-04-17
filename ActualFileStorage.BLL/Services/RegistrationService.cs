using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Adapters;
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
    public class RegistrationService : IRegistrationService //: ISaltResolver, IPasswordHasher OR : IRoleGenerateSalt, IRoleGeneratePassHash
    {
        private ISaltBuilder _saltGen;
        private IPasswordHasher _passHasher;
        private IUserRepository _users;
        private IFolderRepository _folders;
        private IWebRoleRepository _roles;
        public IMapper Mapper { get; }
        public RegistrationService(ISaltBuilder saltGen, IPasswordHasher passHasher, IUserRepository users, IFolderRepository folders, IWebRoleRepository roles, IMapper mapper)
        {
            _saltGen = saltGen;
            _passHasher = passHasher;
            _users = users;
            _folders = folders;
            _roles = roles;
            Mapper = mapper;
        }
        private string GenerateSalt(int size) => _saltGen.GetSalt(size);
        private string GenerateHash(string pass, string salt) => _passHasher.HashPass(pass, salt);
        public void Register(RegistrationUserDTO user, string password)//User u, string password)
        {
            var u = Mapper.Map<User>(user);
            string salt = GenerateSalt(64);
            string hash = GenerateHash(password, salt);
            u.Salt = salt;
            u.PassHash = hash;
            
            Folder privateFolder = CreateRootFolder(u);
            
            _users.Add(u);
            _folders.Add(privateFolder);
            _roles.AddRoleToUser(Role.Default, u);
            _users.SaveChanges();
            _folders.SaveChanges();
            _roles.SaveChanges();
        }
        private Folder CreateRootFolder(User u, FileAccess vis = FileAccess.Private) =>
            new Folder() { Name = u.Login, User = u, Visibility = vis, CreationTime = DateTime.Now };

        public bool LoginExists(string login) => _users.UserWithLoginExists(login);

    }
}
