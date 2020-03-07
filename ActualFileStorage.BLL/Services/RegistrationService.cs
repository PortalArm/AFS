using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Repositories;
using ActualFileStorage.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class RegistrationService //: ISaltResolver, IPasswordHasher OR : IRoleGenerateSalt, IRoleGeneratePassHash
    {
        private ISaltResolver _saltGen;
        private IPasswordHasher _passHasher;
        private IUnitOfWork _uow;
        public RegistrationService(ISaltResolver saltGen, IPasswordHasher passHasher, IUnitOfWork uow)
        {
            _saltGen = saltGen;
            _passHasher = passHasher;
            _uow = uow;
        }
        private string GenerateSalt(int size) => _saltGen.GetSalt(size);
        private string GenerateHash(string pass, string salt) => _passHasher.HashPass(pass, salt);
        public void Register(User u, string password)
        {
            
            var users = _uow.GetRepo<User>();
            string salt = GenerateSalt(64);
            string hash = GenerateHash(password, salt);
            u.Salt = salt;
            u.PassHash = hash;
            Folder privateFolder = CreateRootFolder(u);
            var folders = _uow.GetRepo<Folder>();
            users.Add(u);
            folders.Add(privateFolder);
            _uow.SaveChanges();
            
        }
        private Folder CreateRootFolder(User u, FileAccess vis = FileAccess.Private) =>
            new Folder() { Name = u.Login, User = u, Visibility = vis, CreationTime = DateTime.Now };

        public bool LoginExists(string login)
        {
            var users = _uow.GetRepo<User>();
            return (users as UserRepository).UserWithLoginExists(login);
            //users.
        }

    }
}
