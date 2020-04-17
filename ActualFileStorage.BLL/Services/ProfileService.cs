using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.FileHandlers;
using ActualFileStorage.BLL.Hashers;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.BLL.Verifiers;
using ActualFileStorage.DAL.Models; 
using ActualFileStorage.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IFolderRepository _folders;
        private readonly IUserRepository _users;
        private readonly IFileRepository _files;
        private readonly IStorage _storage;
        private readonly IHash _hash;
        private readonly string rootStringName;
        private readonly IAccessVerifier _verifier;
        public IMapper Mapper { get; }
        public ProfileService(IHash hash, IFolderRepository folders, IFileRepository fileRepo, IUserRepository users, IMapper mapper, IStorage storage, IAccessVerifier verifier)//, string rootAlias)
        {
            _hash = hash;
            _folders = folders;
            Mapper = mapper;
            _storage = storage;
            _users = users;
            _files = fileRepo;
            rootStringName = "root folder";
            _verifier = verifier;
        }

        
        public ObjectsDTO GetObjects(int? callerUserId, int? targetUserId, int? folderId)//, IEnumerable<HistoryItemDTO> history = null)
        {
            if (targetUserId == null)
                if (callerUserId == null)
                    return null;
                else
                    targetUserId = callerUserId;

            var user = _users.GetById(targetUserId.Value);
            var currFolder = folderId.HasValue ? _folders.GetById(folderId.Value) : user.Folder;

            if (currFolder == null)
                return null;
            //var currFolderName = currFolder != null ?
            //    currFolder.Name :
            //    "root";
            //System.IO.File.AppendAllText(@"F:\logs.txt", String.Format("from service:\t\t{0}, {1}, {2}{3}", callerUserId, targetUserId, currFolder.Id, Environment.NewLine));

            var fls = _folders.GetHierarchyToRoot(currFolder.Id);
            var outHistory = Mapper.Map<IEnumerable<HistoryItemDTO>>(fls).Reverse();
            outHistory.First().Value = rootStringName;
            var folders = GetFolders(callerUserId, user, currFolder);
            var files = GetFiles(callerUserId, user, currFolder);

            //var userFolders = _folders.GetUserFoldersIds(targetUserId);
            //var outHistory = ManageHistory(history, folderId, currFolderName);

            return new ObjectsDTO {
                ParentFolderId = currFolder.Id,
                Files = files,
                Folders = folders,
                History = outHistory,
                Parent = Mapper.Map<FolderInfoDTO>(currFolder)
            };
        }

        public FolderInfoDTO GetFolderInfo(int? callerUserId, int folderId)
        {
            var folder = _folders.GetById(folderId);
            var userId = _folders.GetUserIdByFolderId(folderId);
            if (!_verifier.IsAccessibleByVisibility(callerUserId, userId.Value, folder.Visibility))
                return null;

            return Mapper.Map<FolderInfoDTO>(folder);
        }
        public FileInfoDTO GetFileInfo(int? callerUserId, int fileId)
        {
            var file = _files.GetById(fileId);
            var userId = _folders.GetUserIdByFolderId(file.Folder.Id);
            if (!_verifier.IsAccessibleByVisibility(callerUserId, userId.Value, file.Visibility))
                return null;

            var retObject = Mapper.Map<FileInfoDTO>(file);
            if (callerUserId != userId)
                retObject.ReadOnly = true;
            return retObject;
        }
        //private IEnumerable<HistoryItemDTO> ManageHistory(IEnumerable<HistoryItemDTO> history, int? folderId, string folderName)
        //{
        //    if (history == null)
        //        history = Enumerable.Empty<HistoryItemDTO>();

        //    if (!history.Any(i => i.Id == folderId))
        //        history = history.Append(new HistoryItemDTO() { Id = folderId, Value = folderName });
        //    else
        //        history = history.TakeWhileInclude(w => w.Id != folderId);

        //    return history.ToList();
        //}
        //public bool IsAccessibleByVisibility(int? caller, int target, FileAccess vis)
        //{
        //    switch (vis)
        //    {
        //        case FileAccess.Private:
        //            return target == caller;
        //        case FileAccess.Public:
        //            return true;
        //        case FileAccess.RegisteredUsers:
        //            return caller.HasValue;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}
        
        private IEnumerable<FolderDTO> GetFolders(int? callerUserId, User targetUser, Folder folder)
        {
            //здесь не может быть callerUserId == null && targetUser.Id == null
            IEnumerable<Folder> folders = null;

            if (!_verifier.IsAccessibleByVisibility(callerUserId, targetUser.Id, folder.Visibility))
                //throw new UnauthorizedAccessException("Вы попытались получить доступ к папке, к которой не имеете доступа.");
                return null;
            folders = _folders.GetByPredicate(f => f.ParentFolder != null && f.ParentFolder.Id == folder.Id)
                          .Where(f => _verifier.IsAccessibleByVisibility(callerUserId, targetUser.Id, f.Visibility));
            //if (callerUserId == targetUser.Id)
            //    folders = _folders.GetByPredicate(f => f.ParentFolder != null && f.ParentFolder.Id == folder.Id);
            //else

            if (folders == null)
                return null;

            return Mapper.Map<IEnumerable<FolderDTO>>(folders);
        }

        public IEnumerable<FileDTO> GetFiles(int? callerUserId, User targetUser, Folder folder)
        {
            IEnumerable<File> files = null;

            if (!_verifier.IsAccessibleByVisibility(callerUserId, targetUser.Id, folder.Visibility))//IsAccessibleFromUser(callerUserId, folder))
                //throw new UnauthorizedAccessException("Вы попытались получить доступ к папке, к которой не имеете доступа.");
                return null;
            //int? requestedUserId = _folders.GetUserIdByFolderId(folder.Id);

            //if (callerUserId == targetUser.Id)
            files = _files.GetByPredicate(f => f.Folder.Id == folder.Id)
                          .Where(f => _verifier.IsAccessibleByVisibility(callerUserId, targetUser.Id, f.Visibility));
            //files = files.Where(f => IsAccessibleByVisibility(callerUserId, targetUser.Id, f.Visibility));
            //else

            if (files == null)
                return null;

            return Mapper.Map<IEnumerable<FileDTO>>(files);
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
                //нужно добавить, как закончу, т.к. ну или нет, надо подумать
                //sb.Append(generatedFile.Folder.Name);
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
            //if (!_verifier.IsAccessibleByVisibility(parentId, folderName))
            //    return -1;
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
        private bool IsFolderNameAvailable(int parentId, string folderName) => 
            _folders.IsFolderNameAvailable(parentId, folderName);

        //private bool IsAccessibleFromUser(int? userId, Folder folderId) => false;
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
            //!!!!
            var file = _files.GetById(fileId);
            var user = _users.GetById(userId);

            //изменить, UPD: вроде норм
            if (_verifier.IsAccessibleByVisibility(callerId,userId, file.Visibility))
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

        public bool TryCreateLink(int objectId, string link, bool isFile, out string error)
        {
            error = string.Empty;
            if (!Regex.Match(link, @"[A-Za-z0-9_]+").Value.Equals(link))
            {
                error = "Разрешено вводить только набор символов/цифр";
                return false;
            }

            var file = _files.GetByPredicate(w => w.ShortLink != null && w.ShortLink.Equals(link)).FirstOrDefault();
            bool isPresent = file == null ? false : file.Id != objectId;

            if (!isPresent)
            {
                var folder = _folders.GetByPredicate(w => w.ShortLink != null && w.ShortLink.Equals(link)).FirstOrDefault();
                isPresent = folder == null ? false : folder.Id != objectId;
            }

            if (isPresent)
            {
                error = "Выберите другое сокращение";
                return false;
            }

            if (isFile)
            {
                var f = _files.GetById(objectId);
                f.ShortLink = link;
                _files.SaveChanges();
            } else
            {
                var f = _folders.GetById(objectId);
                f.ShortLink = link;
                _folders.SaveChanges();
            }
            return true;
        }

        public void ChangeAccess(int objectId, FileAccess level, bool isFile)
        {
            if (!isFile)
            {
                var folder = _folders.GetById(objectId);
                folder.Visibility = level;
                _folders.SaveChanges();
            }
            else
            {
                var file = _files.GetById(objectId);
                file.Visibility = level;
                _files.SaveChanges();
            }
        }
    }
}
