﻿using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        // GET: Auth
        public ActionResult Index()
        {
            return RedirectToAction("AuthForm");
        }


        [AllowAnonymous]
        public ActionResult AuthForm()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AuthAjaxForm(AuthUserViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("AuthForm");//Json("Ошибка!", JsonRequestBehavior.AllowGet);

            var res = _service.Auth(model.Value, model.Password);
            if (res == null)
            {
                
                ModelState.AddModelError("Password", "Не найдена комбинация логин/пароль");
                return PartialView("AuthForm");//Json(new { status = "error", view = PartialView("AuthForm")},JsonRequestBehavior.AllowGet);
            }
            //здесь авторизацию
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Logout()
        {
            return RedirectToAction("Index", controllerName: "Home");
        }

    }
}