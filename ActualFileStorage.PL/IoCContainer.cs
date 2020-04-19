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
            var config = new MapperConfiguration(cfg =>
                cfg.AddMaps(GetType().Assembly)
            );
            
            IMapper mapper = config.CreateMapper();
            this.RegisterInstance(mapper);
            this.RegisterType<DbContext, FileStorageContext>();
            this.RegisterInstance<IAdapter>(this.Resolve<EFAdapter>());
            
            AddExtension(new ServicesExtension());
            AddExtension(new RepositoriesExtension());
            AddExtension(new BLLExtension());
        }
    }
}