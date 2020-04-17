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

        public IEnumerable<int> GetDescendantFoldersIds(int folderId)
        {
            return _adapter.ExecuteSql<int>("select * from getDescendantFoldersIds(@id)",
                new SqlParameter("@id", folderId)).ToList();
        }

        public IEnumerable<int> GetUserFoldersIds(int userId)
        {
            return _adapter.ExecuteSql<int>("getAllUserFolders @id",
                new SqlParameter("@id", userId)).ToList();
        }

        public IEnumerable<ShortFolder> GetHierarchyToRoot(int folderId)
        {
            return _adapter.ExecuteSql<ShortFolder>("select * from getFoldersIdsToRoot(@id)",
                new SqlParameter("@id", folderId)).ToList();
        }

        public Folder GetFolderByLink(string link)
        {
            return Adapter.ExecuteSqlAsTracked("select * from Folders where ShortLink = @l",
                new SqlParameter("@l", link)).Cast<Folder>().FirstOrDefault();
        }

        public int? GetUserIdByFolderId(int folderId)
        {
            return Adapter.ExecuteSql<int?>("select dbo.getFolderOwnerId(@id)",
                new SqlParameter("@id", folderId)).FirstOrDefault();

        }
        public bool IsFolderNameAvailable(int parentId, string name) => !GetByPredicate(w => w.ParentFolder != null && w.ParentFolder.Id == parentId).Any(z => z.Name.Equals(name));

    }
}
