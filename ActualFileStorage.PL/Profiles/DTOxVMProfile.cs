using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Profiles
{
    public class DTOxVMProfile : Profile
    {
        public DTOxVMProfile()
        {
            CreateMap<FolderDTO, FolderViewModel>();
            CreateMap<FileDTO, FileViewModel>()
                .ForMember(m => m.FullName, opt => opt.MapFrom(mf => System.IO.Path.ChangeExtension(mf.Name, mf.Extension)));
            CreateMap<UserDTO, UserViewModel>();
            CreateMap<FolderInfoDTO, FolderInfoViewModel>();
            CreateMap<ObjectsDTO, ObjectsViewModel>()
                .ForMember(m => m.ParentFolderInfo, opt => opt.MapFrom(mf => mf.Parent));
            CreateMap<ViewIdDTO, ViewIdViewModel>();

            //refactor
            CreateMap<HistoryItemDTO, HistoryItemViewModel>();
                //.ForMember(v => v.id, opt => opt.MapFrom(v => v.Id))
                //.ForMember(v => v.value, opt => opt.MapFrom(v => v.Value))
                //    .ReverseMap()
                //        .ForMember(v => v.Id, opt => opt.MapFrom(v => v.id))
                //        .ForMember(v => v.Value, opt => opt.MapFrom(v => v.value));
            CreateMap<ChangeRoleViewModel, ChangeRoleDTO>()
                .ForMember(m => m.Roles, opt => opt.MapFrom(mf => mf.Roles.ToDictionary(k => k.Role, v => v.Enabled)));
            Func<FileInfoDTO, FileInfoViewModel, object, ResolutionContext, object> func = (one, two, _, context) => {
                string[] sizes = new[] { "B", "KB", "MB", "GB", "TB" };
                int index = 0;
                decimal size = one.Size;
                while (size >= 1024)
                {
                    size /= 1024;
                    index++;
                }
                //res = size /= 1.000000000000000000000000000000000m;
                string res = size.ToString();
                string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                if (res.Contains(separator))
                    res = res.TrimEnd('0').TrimEnd(separator.ToCharArray());

                return res + " " + sizes[index];
            };
            CreateMap<FileInfoDTO, FileInfoViewModel>()
                .ForMember(w => w.FullName, opt => opt.MapFrom(mf => System.IO.Path.ChangeExtension(mf.Name , mf.Extension)))
                .ForMember(m => m.Size, opt => opt.MapFrom(func))
                .ForMember(m => m.ReadOnlyLink, opt => opt.MapFrom(mf => mf.ReadOnly));

            CreateMap<HttpPostedFileBase, FileUploadDTO>()
                .ForMember(m => m.Data, opt => opt.MapFrom(mf => mf.InputStream));

            CreateMap<RegistrationUserViewModel, RegistrationUserDTO>();
        }
    }
}