using ActualFileStorage.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Extension;
using Unity;

namespace ActualFileStorage.PL.UnityExtensions
{
    public class RepositoriesExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<IFolderRepository, FolderRepository>();
            Container.RegisterType<IFileRepository, FileRepository>();
            Container.RegisterType<IWebRoleRepository, WebRoleRepository>();
        }
    }
}