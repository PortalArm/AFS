using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Services;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class RegisterController : Controller
    {
        private IRegistrationService _service;
        private IMapper _mapper {
            get => _service.Mapper;
        }
        public RegisterController(IRegistrationService service)
        {
            _service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegForm()
        {
            return PartialView();
        }
        public JsonResult IsLoginPresent(string login)
        {
            if (_service.LoginExists(login))
                return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegAjaxForm(RegistrationUserViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);

            var u = _mapper.Map<RegistrationUserDTO>(model);
            _service.Register(u, model.Password);

            return Json(new { status = "ok" } , JsonRequestBehavior.AllowGet);
        }

    }
}