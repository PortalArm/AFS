using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class AdminPanelController : Controller
    {
        IAdminService _service;
        IMapper _mapper;
        public AdminPanelController(IAdminService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var d = User.Identity;
            var userInfos = _mapper.Map<IEnumerable<UserViewModel>>(_service.GetUsers());
            return View(userInfos);
        }

        [HttpPost]
        public ActionResult UpdateRoles(IEnumerable<ChangeRoleViewModel> Values)
        {
            _service.UpdateRoles(_mapper.Map<IEnumerable<ChangeRoleDTO>>(Values));
            //if(Values != null)
                //foreach(var vm in Values)
                //     _service.UpdateRoles(vm.Id, vm.Roles.ToDictionary(k => (ActualFileStorage.DAL.Models.Role) k.Role, v => v.Enabled));
            return RedirectToAction("Index");
        }

    }

    
}

////"[{"id":"2","roles":[{"role":"Administrator","enabled":false},{"role":"Moderator","enabled":true},{"role":"Default","enabled":false},{"role":"Guest","enabled":false}]}]"