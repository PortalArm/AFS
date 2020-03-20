using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.DAL.Models;
using AutoMapper;
using System.Linq;

namespace ActualFileStorage.PL.Profiles
{
    public class DomainxDTOProfile : Profile
    {
        public DomainxDTOProfile()
        {
            CreateMap<Folder, FolderDTO>();
            CreateMap<File, FileDTO>().ForMember(m => m.Extension, opt => opt.MapFrom(r => r.Ext));
            CreateMap<User, UserDTO>().ForMember(m => m.Roles, opt => opt.MapFrom(w => w.Roles.Select(z => z.Description)));
            CreateMap<File, FileInfoDTO>()
                .ForMember(m => m.Extension, opt => opt.MapFrom(mf => mf.Ext));
        }
    }
}