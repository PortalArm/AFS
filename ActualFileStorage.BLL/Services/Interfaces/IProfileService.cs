using ActualFileStorage.BLL.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services.Interfaces
{
    public interface IProfileService
    {
        IMapper Mapper { get; }
        int AddFolder(int userId, int parentId, string folderName);
        FileInfoDTO GetFileInfo(int? callerUserId, int fileId);

        //IEnumerable<FileDTO> GetFiles(int userId, int? folderId);
        //IEnumerable<FolderDTO> GetFolders(int userId, int? folderId);
        ObjectsDTO GetObjects(int? callerUserId, int? targetUserId, int? folderId, IEnumerable<HistoryItemDTO> history = null);
        //bool IsFolderNameAvailable(int parentId, string folderName);
        void UploadFiles(int userId, int? folderId, IEnumerable<FileUploadDTO> files);
        void RemoveFolder(int userId, int folderId);
        void RemoveFile(int userId, int fileId);
        FileDownloadDTO DownloadFile(int? callerId, int userId, int fileId);
    }
}
