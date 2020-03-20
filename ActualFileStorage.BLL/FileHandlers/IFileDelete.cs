using ActualFileStorage.DAL.Models;
using System.Collections.Generic;

namespace ActualFileStorage.BLL.FileHandlers
{
    public interface IFileDelete
    {
        void DeleteFile(User user, File file);
        void DeleteFiles(User user, IEnumerable<File> files);
    }
}