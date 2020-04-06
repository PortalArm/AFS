using ActualFileStorage.BLL.FileHandlers;
using ActualFileStorage.BLL.Hashers;
using ActualFileStorage.BLL.Passwords;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.PL.Models;
using ActualFileStorage.PL.UnityExtensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Unity;

namespace ActualFileStorage.PL
{
    public class IoCContainer : UnityContainer
    {
        public IoCContainer()
        {
            var config = new MapperConfiguration(cfg => {
                //!!!
                //cfg.CreateMap<RegistrationUserViewModel, User>();
                cfg.AddMaps(GetType().Assembly);
            });
            
            IMapper mapper = config.CreateMapper();
            this.RegisterInstance(mapper);
            this.RegisterType<DbContext, FileStorageContext>();
            this.RegisterInstance<IAdapter>(this.Resolve<EFAdapter>());//new EFAdapter(new FileStorageContext()));
            

            AddExtension(new ServicesExtension());
            AddExtension(new RepositoriesExtension());
            AddExtension(new BLLExtension());

            //no
            //this.RegisterType<IRepository<User>, UserRepository>();
            //RegistrationService rs = this.Resolve<RegistrationService>();
            //this.RegisterInstance<IRoleGenerateSalt>(rs);
            //this.RegisterInstance<IRoleGeneratePassHash>(rs);

        }
    }
}