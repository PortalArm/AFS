using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //UserCredential GetUserCredsById(int id);
        bool UserWithLoginExists(string login);
        User GetUserByLogin(string login);
        
        // под вопросом
        //Folder GetRootFolderById(int id);
    }
}
