using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
namespace ActualFileStorage.DAL.Repositories
{
    class UserRepository : Repository, IUserRepository
    {

        public UserRepository(IAdapter context) : base(context)
        {
            
        }

        public void Add(User obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetByPredicate(Func<User, bool> pred)
        {
            throw new NotImplementedException();
        }

        public UserCredential GetUserCredsById(int id)
        {
            User obj = (User)_adapter.Find(id);

            return new UserCredential() {
                Email = obj.Email,
                Login = obj.Login,
                PassHash = obj.PassHash,
                UserId = obj.Id
            };
        }

        public void Remove(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
