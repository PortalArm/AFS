using ActualFileStorage.DAL.Models;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.Configurations.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationUserViewModel, User>();
            CreateMap<User, UserViewModel>().ForMember("RootFolderAccess", opt => opt.MapFrom(r => r.Folder.Visibility));
        }
    }
}
