using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class ShortenerController : Controller
    {
        // GET: Shortener
        public ActionResult Unpack(object id)
        {
            Console.WriteLine($"Received {id}");
            return View(id);
        }
    }
}