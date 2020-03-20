using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Extensions;
using ActualFileStorage.BLL.FileHandlers;
using ActualFileStorage.BLL.Hashers;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IFolderRepository _folders;
        private readonly IUserRepository _users;
        private readonly IFileRepository _files;
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly IHash _hash;
        public IMapper Mapper { get => _mapper; }

        public ProfileService(IHash hash, IFolderRepository folders, IFileRepository fileRepo, IUserRepository users, IMapper mapper, IStorage storage)
        {
            _hash = hash;
            _folders = folders;
            _mapper = mapper;
            _storage = storage;
            _users = users;
            _files = fileRepo;
        }

        public ObjectsDTO GetObjects(int? callerUserId, int? targetUserId, int? folderId, IEnumerable<HistoryItemDTO> history = null)
        {
            if (targetUserId == null)
                if (callerUserId == null)
                    return null;
                else
                    targetUserId = callerUserId;

            var user = _users.GetById(targetUserId.Value);
            var currFolder = folderId.HasValue ? _folders.GetById(folderId.Value) : user.Folder;

            var currFolderName = currFolder != null ?
                currFolder.Name :
                "root";

            var folders = GetFolders(callerUserId, user, currFolder);
            var files = GetFiles(callerUserId, user, currFolder);

            //var userFolders = _folders.GetUserFoldersIds(targetUserId);
            var outHistory = ManageHistory(history, folderId, currFolderName);

            return new ObjectsDTO {
                ParentFolderId = currFolder.Id,
                Files = files,
                Folders = folders,
                History = outHistory
            };
        }

        public FileInfoDTO GetFileInfo(int? callerUserId, int fileId)
        {
            var file = _files.GetById(fileId);
            if (!IsAccessibleFromUser(callerUserId, file))
                return null;
            return _mapper.Map<FileInfoDTO>(file);
        }
        private IEnumerable<HistoryItemDTO> ManageHistory(IEnumerable<HistoryItemDTO> history, int? folderId, string folderName)
        {
            if (history == null)
                history = Enumerable.Empty<HistoryItemDTO>();

            if (!history.Any(i => i.Id == folderId))
                history = history.Append(new HistoryItemDTO() { Id = folderId, Value = folderName });
            else
                history = history.TakeWhileInclude(w => w.Id != folderId);

            return history.ToList();
        }
        private IEnumerable<FolderDTO> GetFolders(int? callerUserId, User targetUser, Folder folder)
        {
            //здесь не может быть callerUserId == null && targetUser.Id == null
            IEnumerable<Folder> folders = null;

            if (!IsAccessibleFromUser(callerUserId, folder))
                throw new UnauthorizedAccessException("Вы попытались получить доступ к папке, к которой не имеете доступа.");

            if (callerUserId == targetUser.Id)
                folders = _folders.GetByPredicate(f => f.ParentFolder != null && f.ParentFolder.Id == folder.Id);
            //else

            if (folders == null)
                return null;

            return _mapper.Map<IEnumerable<FolderDTO>>(folders);

        }

        public IEnumerable<FileDTO> GetFiles(int? callerUserId, User targetUser, Folder folder)
        {
            IEnumerable<File> files = null;

            if (!IsAccessibleFromUser(callerUserId, folder))
                throw new UnauthorizedAccessException("Вы попытались получить доступ к папке, к которой не имеете доступа.");

            if (callerUserId == targetUser.Id)
                files = _files.GetByPredicate(f => f.Folder.Id == folder.Id);
            //else

            if (files == null)
                return null;

            return _mapper.Map<IEnumerable<FileDTO>>(files);
        }

        public void UploadFiles(int userId, int? folderId, IEnumerable<FileUploadDTO> files)
        {
            var user = _users.GetById(userId);
            folderId = folderId.HasValue ? folderId : user.Folder.Id;
            var folder = _folders.GetById(folderId.Value);
            foreach (var file in files)
            {
                //переделать под automapper
                File generatedFile = new File() {
                    Folder = folder,
                    CreationTime = DateTime.Now,
                    Name = System.IO.Path.GetFileNameWithoutExtension(file.FileName),
                    Ext = System.IO.Path.GetExtension(file.FileName),
                    Size = file.Data.Length,
                    Visibility = FileAccess.Private
                };
                StringBuilder sb = new StringBuilder();
                sb.Append(generatedFile.Name);
                sb.Append(generatedFile.Size * 31);
                sb.Append(generatedFile.Ext);
                sb.Append(generatedFile.CreationTime.ToString());
                generatedFile.Hash = _hash.Hash(sb.ToString());

                _files.Add(generatedFile);
                _storage.UploadFile(user, generatedFile, file.Data);
                _files.SaveChanges();
            }
        }

        public int AddFolder(int userId, int parentId, string folderName)
        {
            var user = _users.GetById(userId);
            var parentFolder = _folders.GetById(parentId);
            if (!IsFolderNameAvailable(parentId, folderName))
                return -1;
            Folder f = new Folder() {
                CreationTime = DateTime.Now,
                Name = folderName,
                ParentFolder = parentFolder,
                Visibility = FileAccess.Private//,
                //User = user
            };
            _folders.Add(f);
            _folders.SaveChanges();
            return f.Id;
        }
        private bool IsFolderNameAvailable(int parentId, string folderName) => _folders.IsFolderNameAvailable(parentId, folderName);

        private bool IsAccessibleFromUser(int? userId, Folder folderId) => true;
        private bool IsAccessibleFromUser(int? userId, File fileId) => true;

        public void RemoveFolder(int userId, int folderId)
        {
            IEnumerable<File> files = _files.GetAllFilesInFolder(folderId).ToList();
            IEnumerable<int> foldersIds = _folders.GetDescendantFoldersIds(folderId).ToList();
            //!!
            IEnumerable<Folder> folders = foldersIds.Select(id => _folders.GetById(id)).ToList();

            foreach (var file in files)
                _files.Remove(file);
            foreach (var folder in folders)
                _folders.Remove(folder);

            var user = _users.GetById(userId);
            _storage.DeleteFiles(user, files);
            _files.SaveChanges();
            _folders.SaveChanges();
        }

        public FileDownloadDTO DownloadFile(int? callerId, int userId, int fileId)
        {
            var file = _files.GetById(fileId);
            var user = _users.GetById(userId);

            if (IsAccessibleFromUser(callerId, file)) 
                return new FileDownloadDTO() {
                    Data = _storage.DownloadFile(user, file),
                    FileName = System.IO.Path.ChangeExtension(file.Name, file.Ext),
                    IsPermitted = true
                };
            return new FileDownloadDTO() {
                IsPermitted = false
            };
        }
        public void RemoveFile(int userId, int fileId)
        {
            File file = _files.GetById(fileId);
            _files.Remove(file);

            var user = _users.GetById(userId);
            _storage.DeleteFiles(user, new[] { file });
            _files.Remove(file);
            _files.SaveChanges();
        }


    }
}
