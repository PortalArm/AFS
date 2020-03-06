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
            //test data
            //т.к. пока нет авторизации
            //var serviceFolders = _service.GetFolders(3, null);
            //var serviceFiles = _service.GetFiles(3, null);
            //var serviceFilesYetAgain = _service.GetFiles(3, serviceFolders.ElementAt(0).Id);

            //var folders = _mapper.Map<IEnumerable<FolderViewModel>>(serviceFolders);
            //var files1 = _mapper.Map<IEnumerable<FileViewModel>>(serviceFiles);
            //var files2 = _mapper.Map<IEnumerable<FileViewModel>>(serviceFilesYetAgain);
            //var objects = new ObjectsViewModel() {
            //    Folders = folders,
            //    Files = files1.Concat(files2)
            //};
            return View();/*View();// RedirectToAction("GetContent", new { userId = 3, folderId = (int?)null }) ;*/
        }
        //public ActionResult GetContent(int? id)
        //{
        //    var userId = id.HasValue ? id.Value : 3;
        //    var folderId = (int?)null;
        //    //test data
        //    //т.к. пока нет авторизации
        //    var serviceFolders = _service.GetFolders(userId, folderId);
        //    var serviceFiles = _service.GetFiles(userId, folderId);
        //    //var serviceFilesYetAgain = _service.GetFiles(userId, serviceFolders.ElementAt(0).Id);

        //    var folders = _mapper.Map<IEnumerable<FolderViewModel>>(serviceFolders);
        //    var files = _mapper.Map<IEnumerable<FileViewModel>>(serviceFiles);
        //    //var files2 = _mapper.Map<IEnumerable<FileViewModel>>(serviceFilesYetAgain);
        //    var objects = new ObjectsViewModel() {
        //        Folders = folders,
        //        Files = files//.Concat(files2)
        //    };
        //    return PartialView(objects);
        //}
        [HttpPost]
        public ActionResult GetContent(int? id, int? folderId, IEnumerable<string> history)
        {
            var userId = id.HasValue ? id.Value : 3;
            //test data
            //т.к. пока нет авторизации

            var objectsUnmapped = _service.GetObjects(id.Value, folderId, history.Select(s => {
                if (int.TryParse(s, out int val))
                    return val;
                return (int?)null;
            }));
            var objects = _mapper.Map<ObjectsViewModel>(objectsUnmapped);
            //var serviceFolders = _service.GetFolders(userId, folderId);
            //var serviceFiles = _service.GetFiles(userId, folderId);

            //var folders = _mapper.Map<IEnumerable<FolderViewModel>>(serviceFolders);
            //var files = _mapper.Map<IEnumerable<FileViewModel>>(serviceFiles);
            //var objects = new ObjectsViewModel() {
            //    Folders = folders,
            //    Files = files
            //};
            return PartialView(objects);
        }


        public JsonResult RenderTree(int? id)
        {
            return Json(new { data = 0 });
        }
    }
}