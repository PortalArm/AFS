using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        
        void SaveChanges();
    }
}
