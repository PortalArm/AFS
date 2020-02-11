using ActualFileStorage.BLL.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class ShortenerController : Controller
    {
        private ILinkResolver link;
        public ShortenerController(ILinkResolver resolver)
        {
            link = resolver;
        }
        // GET: Shortener
        public ActionResult Unpack(object id)
        {
            Console.WriteLine("{0} {1}", link.Decode(id.ToString()), link.Encode(link.Decode(id.ToString())));
            Console.WriteLine($"Received {id}");
            return View(id);
        }
    }
}