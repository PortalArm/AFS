using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.UOW;
using ActualFileStorage.PL.Models;
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
        public AdminPanelController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult Index()
        {
            var users = _uow.GetRepo<User>();
            var userInfos = users.GetAll().Select(u => new UserViewModel() {
                BirthDate = u.BirthDate,
                Email = u.Email,
                FirstName = u.FirstName,
                Id = u.Id,
                Login = u.Login,
                RootFolderAccess = u.Folder.Visibility,
                SecondName = u.SecondName
            });

            return View(userInfos);
        }
    }
}