using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public class FolderRepository : Repository<Folder>, IFolderRepository
    {
        public FolderRepository(IAdapter adapter) : base(adapter)
        {
        }

        public IEnumerable<int> GetUserFoldersIds(int userId)
        {
            return _adapter.ExecuteSql<int>("getAllUserFolders @id", new SqlParameter("id", userId)).Cast<int>();
        }
    }
}
