using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.Configurations.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Initialize()
        {
            return new MapperConfiguration(
                cfg => cfg.AddMaps(typeof(AutoMapperConfig).Assembly)
                );
        }
    }
}
