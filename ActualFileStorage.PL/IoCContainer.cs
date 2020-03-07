using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.FileHandlers;
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
                cfg.CreateMap<Folder, FolderDTO>();
                cfg.CreateMap<File, FileDTO>().ForMember(m => m.Extension, opt => opt.MapFrom(r => r.Ext));

                //cfg.CreateMap<ObjectBase, ObjectBaseViewModel>();
                    //.Include<FolderDTO, FolderViewModel>()
                    //.Include<FileDTO, FileViewModel>();

                cfg.CreateMap<FolderDTO, FolderViewModel>();
                cfg.CreateMap<FileDTO, FileViewModel>();

                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<ObjectsDTO, ObjectsViewModel>();

                cfg.CreateMap<HistoryItemDTO, HistoryItemViewModel>()
                    .ForMember(v => v.id, opt => opt.MapFrom(v => v.Id))
                    .ForMember(v => v.value, opt => opt.MapFrom(v => v.Value))
                        .ReverseMap()
                        .ForMember(v => v.Id, opt => opt.MapFrom(v => v.id))
                        .ForMember(v => v.Value, opt => opt.MapFrom(v => v.value));


                //cfg.CreateMap<HistoryItemViewModel, HistoryItemDTO>();
            });
            
            IMapper mapper = config.CreateMapper();
            this.RegisterInstance(mapper);
            this.RegisterType<IFileRoutine, LocalServerStorage>();
            this.RegisterType<IAdapter, EFAdapter>();
            this.RegisterType<DbContext, FileStorageContext>();
            this.RegisterType<IUnitOfWork, BasicUOW>();
            this.RegisterType<ILinkResolver, MockLink>();
            this.RegisterType<ISaltResolver, SaltResolver>();
            this.RegisterType<IPasswordHasher, PasswordHasher>();
            this.RegisterType<IAuthService, AuthService>();
            this.RegisterType<IAdminService, AdminService>();
            this.RegisterType<IProfileService, ProfileService>();
            this.RegisterType<IUserRepository, UserRepository>();
            this.RegisterType<IFolderRepository, FolderRepository>();
            this.RegisterType<IFileRepository, FileRepository>();


            //this.RegisterType<IRepository<User>, UserRepository>();
            //RegistrationService rs = this.Resolve<RegistrationService>();
            //this.RegisterInstance<IRoleGenerateSalt>(rs);
            //this.RegisterInstance<IRoleGeneratePassHash>(rs);

        }
    }
}