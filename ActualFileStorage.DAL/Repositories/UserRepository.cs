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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IAdapter adapter) : base(adapter)
        {

        }

        public Folder GetRootFolderById(int id)
        {

            User obj = (User)Adapter.Find(id);
            return obj.Folder;
        }

        public UserCredential GetUserCredsById(int id)
        {
            var tt = Adapter.Find(id);
            User obj = (User)Adapter.Find(id);

            return new UserCredential() {
                Email = obj.Email,
                Login = obj.Login,
                PassHash = obj.PassHash,
                UserId = obj.Id
            };
        }


    }
}
