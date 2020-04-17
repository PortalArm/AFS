using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(IAdapter adapter) : base(adapter)
        {

        }

        public IEnumerable<File> GetAllFilesInFolder(int folderId)
        {
            return Adapter.ExecuteSqlAsTracked("select * from getDescendantFilesIds(@id)",
                new System.Data.SqlClient.SqlParameter("@id", folderId)).Cast<File>();
        }

        public File GetFileByLink(string link)
        {
            return Adapter.ExecuteSqlAsTracked("select * from Files where ShortLink = @l",
                new System.Data.SqlClient.SqlParameter("@l", link)).Cast<File>().FirstOrDefault();
        }

    }
}
