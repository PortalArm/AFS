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
    public class ProfileController : Controller
    {

        private IProfileService _service;
        private IMapper _mapper;
        public ProfileController(IProfileService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: Profile
        public ActionResult Index()
        {
            var fols = _service.GetFolders(3, null);
            var folders = _mapper.Map<IEnumerable<FolderViewModel>>(fols);
            return View(folders);
        }

        
        public JsonResult RenderTree(int? id)
        {
            return Json(new { data = 0 });
        }
    }
}