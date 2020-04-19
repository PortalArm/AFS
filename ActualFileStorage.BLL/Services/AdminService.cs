using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.Repositories;
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
        private IUserRepository _users;
        private IWebRoleRepository _roles;
        public IMapper Mapper { get; }
        public AdminService(IUserRepository users, IWebRoleRepository roles, IMapper mapper)
        {
            _users = users;
            Mapper = mapper;
            _roles = roles;
        }

        //public void UpdateRoles(int userId, Dictionary<Role, bool> roles)
        //{
        //    var user = _users.GetById(userId);
        //    foreach (Role role in roles.Keys)
        //        if (roles[role])
        //            _roles.AddRoleToUser(role, user);
        //        else
        //            _roles.RemoveRoleFromUser(role, user);

        //    _roles.SaveChanges();

        //}
        public void UpdateRoles(IEnumerable<ChangeRoleDTO> roles) {
            foreach(var rolelist in roles) {
                var user = _users.GetById(rolelist.Id);
                foreach (Role role in rolelist.Roles.Keys)
                    if (rolelist.Roles[role])
                        _roles.AddRoleToUser(role, user);
                    else
                        _roles.RemoveRoleFromUser(role, user);
            }
            _roles.SaveChanges();
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            return Mapper.Map<IEnumerable<UserDTO>>(_users.GetAll());
        }

    }
}
