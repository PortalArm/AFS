using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Extensions;
using ActualFileStorage.BLL.FileHandlers;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Repositories;
using ActualFileStorage.DAL.UOW;
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
        private IFolderRepository _folders;
        private IUserRepository _users;
        private IFileRepository _fileRepo;
        private IMapper _mapper;
        private IFileRoutine _file;
        public ProfileService(/*IUnitOfWork uow*/IFolderRepository folders,IFileRepository fileRepo, IUserRepository users, IMapper mapper, IFileRoutine file)
        {
            _folders = folders;  _mapper = mapper; _file = file; _users = users; _fileRepo = fileRepo;
        }

        public ObjectsDTO GetObjects(int userId, int? folderId, IEnumerable<HistoryItemDTO> history = null)
        {
            var folders = GetFolders(userId, folderId); 
            var files = GetFiles(userId, folderId);
            history = history.ToList();
            var currFolderName = folderId.HasValue?
                _folders.GetById(folderId.Value).Name:
                "root";
            var userFolders = _folders.GetUserFoldersIds(userId);
            if (history == null)
                throw new DataMisalignedException("Этого быть не должно");
            //var skip = history.TakeWhileInclude(w => !w.Equals(folderId)).ToList();
            //if (history.Count() == skip.Count())
            if(!history.Any(i => i.Id == folderId))
                history = history.Append(new HistoryItemDTO() { Id = folderId, Value = currFolderName });
            else
                history = history.TakeWhileInclude(w => w.Id != folderId);
            history = history.ToList();
            return new ObjectsDTO {
                Files = files,
                Folders = folders,
                History = history
            };
        }
        public IEnumerable<FolderDTO> GetFolders(int userId, int? folderId)
        {
            
            
            //int ogo = ddd.Count();
            var user = _users.GetById(userId);
            if (!folderId.HasValue)
                folderId = user.Folder.Id;
            
            if (!IsAccessibleFromUser(userId, folderId.Value))
                throw new UnauthorizedAccessException("Вы попытались получить доступ к папке, к которой не имеете доступа.");

            var initFols = _folders.GetByPredicate(f => f.ParentFolder != null && f.ParentFolder.Id == folderId.Value);
            if (initFols == null)
                return null;

            return _mapper.Map<IEnumerable<FolderDTO>>(initFols);

        }

        public IEnumerable<FileDTO> GetFiles(int userId, int? folderId)
        {
            var user = _users.GetById(userId);
            if (!folderId.HasValue)
                folderId = user.Folder.Id;

            if (!IsAccessibleFromUser(userId, folderId.Value))
                throw new UnauthorizedAccessException("Вы попытались получить доступ к файлу, к которому не имеете доступа.");

            var initFiles = _fileRepo.GetByPredicate(f => f.Folder.Id == folderId.Value);
            if (initFiles == null)
                return null;

            return _mapper.Map<IEnumerable<FileDTO>>(initFiles);

        }

        public bool IsAccessibleFromUser(int userId, int folderId)
        {
            return true;
        }
    }
}
