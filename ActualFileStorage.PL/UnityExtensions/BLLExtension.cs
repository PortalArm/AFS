using ActualFileStorage.BLL.FileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Extension;
using Unity;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Hashers;
using ActualFileStorage.BLL.Verifiers;

namespace ActualFileStorage.PL.UnityExtensions
{
    public class BLLExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var afs = new AzureFileStorage();
            // hmm
            Container.RegisterInstance<IStorage>(afs);
            //this.RegisterType<ILinkBuilder, MockLink>();
            Container.RegisterType<ISaltBuilder, SaltBuilder>();
            Container.RegisterType<IPasswordHasher, PasswordHasher>();
            //thisis.RegisterType<IHash, SHA256Hash>();
            Container.RegisterInstance<IHash>(new SHA256Hash());//(this.Resolve<SHA256Hash>());
            Container.RegisterType<IAccessVerifier, AccessVerifier>();
        }
    }
}