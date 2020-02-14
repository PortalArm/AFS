using ActualFileStorage.BLL.Links;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace ActualFileStorage.PL
{
    public class IoCContainer : UnityContainer
    {
        public IoCContainer()
        {
            this.RegisterType<ILinkResolver, MockLink>();
            this.RegisterType<ISaltResolver, SaltResolver>();
            this.RegisterType<IPasswordHasher, PasswordHasher>();
            
            //RegistrationService rs = this.Resolve<RegistrationService>();
            //this.RegisterInstance<IRoleGenerateSalt>(rs);
            //this.RegisterInstance<IRoleGeneratePassHash>(rs);
            
            //this.RegisterType<IAdapter, EFAdapter>();

        }
    }
}