using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Services.Description;

namespace ActualFileStorage.PL.Controllers
{
    public class RegController : ApiController
    {
        private IRegistrationService _service;
        public RegController(IRegistrationService service)
        {
            _service = service;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //public IHttpActionResult IsLoginPresent(string login)
        //{
        //    if (_service.LoginExists(login))
        //        return Json("This login is already occupied.");
        //        //return Json("This login is already occupied.", JsonRequestBehavior.AllowGet);

        //    return Json(true);
        //}
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}