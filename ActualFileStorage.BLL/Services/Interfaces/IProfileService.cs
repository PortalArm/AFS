using ActualFileStorage.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IProfileService
    {
        IEnumerable<FileDTO> GetFiles(int userId, int? folderId);
        IEnumerable<FolderDTO> GetFolders(int userId, int? folderId);
        ObjectsDTO GetObjects(int userId, int? folderId, IEnumerable<HistoryItemDTO> history = null);
    }
}
