using ActualFileStorage.BLL.Links;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Repositories;
using ActualFileStorage.DAL.UOW;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;

namespace ActualFileStorage.PL
{
    //public class FirstProfile : Profile
    //{
        
    //}
    public class IoCContainer : UnityContainer
    {
        public IoCContainer()
        {
            //MapperConfiguration config = new MapperConfiguration(
            //    cfg => cfg.AddMaps(GetType().Assembly)<RegistrationUserViewModel, User>()
            //    );
            //        MapperConfiguration mapconf = new MapperConfiguration(
            //cfg => cfg.CreateMap< RegistrationUserViewModel, User > ()
            //);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegistrationUserViewModel, User>();
                cfg.CreateMap<User, UserViewModel>().ForMember("RootFolderAccess", opt => opt.MapFrom(r => r.Folder.Visibility));
            });

            IMapper mapper = config.CreateMapper();
            this.RegisterInstance(mapper);
            this.RegisterType<IAdapter, EFAdapter>();
            this.RegisterType<DbContext, FileStorageContext>();
            this.RegisterType<IUnitOfWork, BasicUOW>();
            this.RegisterType<ILinkResolver, MockLink>();
            this.RegisterType<ISaltResolver, SaltResolver>();
            this.RegisterType<IPasswordHasher, PasswordHasher>();

            
            //this.RegisterType<IRepository<User>, UserRepository>();
            //RegistrationService rs = this.Resolve<RegistrationService>();
            //this.RegisterInstance<IRoleGenerateSalt>(rs);
            //this.RegisterInstance<IRoleGeneratePassHash>(rs);

        }
    }
}