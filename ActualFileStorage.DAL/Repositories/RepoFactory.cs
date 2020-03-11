using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    //не по OCP?
    public static class RepoFactory
    {
        public static IRepository<T> Get<T>(IAdapter adapter) where T : class
        {
            Type t = typeof(T);

            if(t == typeof(User))
                return new UserRepository(adapter) as IRepository<T>;

            if (t == typeof(Folder))
                return new FolderRepository(adapter) as IRepository<T>;

            if (t == typeof(WebRole))
                return new WebRoleRepository(adapter) as IRepository<T>;


            return null; 

        }
    }

}
