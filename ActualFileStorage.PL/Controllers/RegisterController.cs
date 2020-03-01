using ActualFileStorage.BLL.Services;
using ActualFileStorage.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class RegisterController : Controller
    {
        RegistrationService _service;
        public RegisterController(RegistrationService service)
        {
            _service = service;
        }
        // GET: Register
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
                return Json("This login is already occupied.", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegAjaxForm(RegistrationUserViewModel model)
        {
            if (!ModelState.IsValid)
                return new JsonResult() { ContentType = "application/json", Data = new { status = "error" } };

            DAL.Models.User u = new DAL.Models.User() {
                BirthDate = model.BirthDate,
                Email = model.Email,
                FirstName = model.FirstName,
                Login = model.Login,
                SecondName = model.SecondName
            };
            _service.Register(u, model.Password);
            return new JsonResult() { ContentType = "application/json", Data = new { status = "ok" } };
            //RedirectToAction("Index", "Home");
        }

    }
}