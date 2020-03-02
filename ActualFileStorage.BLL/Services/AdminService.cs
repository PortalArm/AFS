using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.UOW;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Services
{
    public class AdminService : IAdminService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        public AdminService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _uow.GetRepo<User>();
            return _mapper.Map<UserViewModel>(users.GetAll());
        }


    }
}
