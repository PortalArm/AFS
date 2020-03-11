using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.UOW;
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
        IUnitOfWork _uow;
        IMapper _mapper;
        public AdminPanelController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var users = _uow.GetRepo<User>();
            var userInfos = _mapper.Map<IEnumerable<UserViewModel>>(users.GetAll());
            return View(userInfos);
        }
    }
}