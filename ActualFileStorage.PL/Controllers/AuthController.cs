using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.PL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
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
            ClaimsIdentity ci = new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Name, model.Value),
                new Claim(ClaimTypes.Email, res.Email),
                new Claim(ClaimTypes.DateOfBirth, res.BirthDate.ToString())
            }, ConfigurationManager.AppSettings["authtype"]);
            foreach (var role in res.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            
            //ci.AuthenticationType = ConfigurationManager.AppSettings["authtype"];
            HttpContext.GetOwinContext().Authentication.SignIn(ci);//ci);
            var be = (ci.IsAuthenticated);

            //var rresult = HttpContext.GetOwinContext().Authentication.AuthenticateAsync(ConfigurationManager.AppSettings["authtype"]);
            //rresult.Wait();
            //var result = rresult.Result;

            //HttpContext.GetOwinContext().Authentication.SignIn(result.Identity);//ci);

            //здесь авторизацию
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(ConfigurationManager.AppSettings["authtype"]);
            return RedirectToAction("Index", controllerName: "Home");
        }

    }
}