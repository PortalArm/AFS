using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections;
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
            return View();
        }

        [HttpPost]
        public ActionResult GetContent(int? id, int? folderId, IEnumerable<HistoryItemViewModel> history)
        {
            var userId = id.HasValue ? id.Value : 3;
            //test data
            //т.к. пока нет авторизации

            var objectsUnmapped = _service.GetObjects(id.Value, folderId, _mapper.Map<IEnumerable<HistoryItemDTO>>(history));

            /*
             .Select(s => { 
                if (int.TryParse(s, out int val))
                    return val;
                return (int?)null;
            }
             
             */
            var objects = _mapper.Map<ObjectsViewModel>(objectsUnmapped);

            return PartialView(objects);
        }

        [HttpPost]
        
        public ActionResult UploadFiles(int? folderId, IEnumerable<HttpPostedFileBase> files)
        {
            
            return RedirectToAction("Index");
        }
        public JsonResult RenderTree(int? id)
        {
            return Json(new { data = 0 });
        }
    }
}