using ActualFileStorage.BLL.DTOs;
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
        private IMapper _mapper;
        private IFileRoutine _file;
        public ProfileService(/*IUnitOfWork uow*/IFolderRepository folders, IUserRepository users, IMapper mapper, IFileRoutine file)
        {
            _folders = folders;  _mapper = mapper; _file = file; _users = users;
        }
        public IEnumerable<FolderDTO> GetFolders(int userId, int? folderId)
        {
            var user = _users.GetById(userId);
            if (!folderId.HasValue)
                folderId = user.Folder.Id;
            
            if (!IsAccessibleFromUser(userId, folderId.Value))
                throw new UnauthorizedAccessException("Вы попытались получить доступ к папке, к которой вы не имеете доступа.");

            var initFols = _folders.GetByPredicate(f => f.ParentFolder != null && f.ParentFolder.Id == folderId.Value);
            if (initFols == null)
                return null;

            return _mapper.Map<IEnumerable<FolderDTO>>(initFols);

        }

        public bool IsAccessibleFromUser(int userId, int folderId)
        {
            return true;
        }
    }
}
