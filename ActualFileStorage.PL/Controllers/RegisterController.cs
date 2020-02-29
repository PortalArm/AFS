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
        ActualFileStorage.BLL.Services.RegistrationService _regService;
        public RegisterController(ActualFileStorage.BLL.Services.RegistrationService rs)
        {
            _regService = rs;
        }
        // GET: Register
        public ActionResult Index()
        {
            return PartialView(regs);
        }

        

        public ActionResult Register()
        {
            return View();
        }

        private static List<RegistrationUserViewModel> regs = new List<RegistrationUserViewModel>() {
        new RegistrationUserViewModel() {  Login = "4t3523er", BirthDate = DateTime.Now, FirstName = "4tgrdfg", SecondName = "4etgrdg", Email = "3tgdfg"},
        new RegistrationUserViewModel() {  Login = "4tegfdr", BirthDate = DateTime.Now, FirstName = "4tgrdfg", SecondName = "4etgrdg", Email = "3tgdfg"},
        new RegistrationUserViewModel() {  Login = "4tersger", BirthDate = DateTime.Now, FirstName = "4tgrdfg", SecondName = "4etgrdg", Email = "3tgdfg"},
        new RegistrationUserViewModel() {  Login = "4ter34h6", BirthDate = DateTime.Now, FirstName = "4tgrdfg", SecondName = "4etgrdg", Email = "3tgdfg"}
        };
        [HttpPost]
        public ActionResult Register(RegistrationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                regs.Add(model);
            }
            return PartialView("Index", regs);
            //return RedirectToAction("Index","Home");
        }
    }
}