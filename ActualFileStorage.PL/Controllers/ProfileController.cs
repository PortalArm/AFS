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
            var serviceFolders = _service.GetFolders(3, null);
            var serviceFiles = _service.GetFiles(3, null);
            var serviceFilesYetAgain = _service.GetFiles(3, serviceFolders.ElementAt(0).Id);

            var folders = _mapper.Map<IEnumerable<FolderViewModel>>(serviceFolders);
            var files1 = _mapper.Map<IEnumerable<FileViewModel>>(serviceFiles);
            var files2 = _mapper.Map<IEnumerable<FileViewModel>>(serviceFilesYetAgain);
            var objects = new ObjectsViewModel() {
                Folders = folders,
                Files = files1.Concat(files2)
            };
            return View(objects);
        }

        
        public JsonResult RenderTree(int? id)
        {
            return Json(new { data = 0 });
        }
    }
}