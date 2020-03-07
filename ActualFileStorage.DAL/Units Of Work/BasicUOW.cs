using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.UOW
{
    public class BasicUOW : IUnitOfWork
    {
        private Dictionary<Type, object> _dict = new Dictionary<Type, object>();
        //private IUserRepository _users;
        //private IFolderRepository _folders;
        private IAdapter _adapter;

        public BasicUOW(IAdapter adapter)
        {
            _adapter = adapter;
            //System.IO.File.AppendAllLines(@"C:\Users\Tom\Desktop\Проект_EPAM\logs\log.txt", new[] { $"Constructor of {GetType()} UOW invoked" });

        }
        //public BasicUOW(IUserRepository userRepo, IFolderRepository folderRepo)
        //{
        //    _users = userRepo;
        //    _folders = folderRepo;
        //}

        public void SaveChanges()
        {
            // TODO: Придумать что-то получше
            foreach (IRepository v in _dict.Values)
                v.SaveChanges();
        }

        public IRepository<T> GetRepo<T>() where T : class
        {
            Type repoType = typeof(T);
            if (_dict.ContainsKey(repoType))
                return (IRepository<T>)_dict[repoType];

            return (IRepository<T>)(_dict[repoType] = RepoFactory.Get<T>(_adapter));//new Repository<T>(_adapter));

        }

        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SaveChanges();
                    foreach (IDisposable v in _dict.Values)
                        v?.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
