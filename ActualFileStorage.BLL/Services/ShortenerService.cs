using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.BLL.Verifiers;
using ActualFileStorage.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class ShortenerService : IShortenerService
    {
        private IFolderRepository _folders;
        private IFileRepository _files;
        private IAccessVerifier _verifier;
        public IMapper Mapper { get; }

        public ShortenerService(IFolderRepository folders, IFileRepository files, IMapper mapper, IAccessVerifier verifier)
        {
            _folders = folders;
            _files = files;
            Mapper = mapper;
            _verifier = verifier;
        }


        public ViewIdDTO GetIdsByLink(int? callerId, string link)
        {
            var folder = _folders.GetFolderByLink(link);
            var file = _files.GetFileByLink(link); 
            
            if (folder != null && file != null)
                throw new NotSupportedException("Почему-то существует и файл, и папка с одной и той же ссылкой");

            if (folder == null && file == null)
                return new ViewIdDTO();

            DAL.Models.Folder renderedFolder = file != null ? file.Folder : folder;
            int targetUserId = _folders.GetUserIdByFolderId(renderedFolder.Id).Value;
            bool accessible = _verifier.IsAccessibleByVisibility(callerId, 
                targetUserId, 
                file != null ? file.Visibility : renderedFolder.Visibility);//false;
            //if (file != null)
            //    accessible = _verifier.IsAccessibleByVisibility(callerId, targetUserId, file.Visibility);
            //else
            //    accessible = _verifier.IsAccessibleByVisibility(callerId, targetUserId, renderedFolder.Visibility);

            //if (_verifier.IsAccessibleByVisibility(callerId, targetUserId, renderedFolder.Visibility))
            return new ViewIdDTO() {
                UserId = targetUserId,
                FolderId = renderedFolder.Id,
                IsFile = file != null,
                FileId = file?.Id,
                Exists = accessible
                //File = Mapper.Map<FileInfoDTO>(file)
            };
        }
    }
}
