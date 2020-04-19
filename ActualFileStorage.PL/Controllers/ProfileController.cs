using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.PL.Attributes;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileService _service;
        private ISaltBuilder _urlGenerator; 
        private IMapper _mapper {
            get => _service.Mapper;
        }
        public ProfileController(IProfileService service, ISaltBuilder urlGenerator)
        {
            _service = service;
            _urlGenerator = urlGenerator;
        }
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return View(model: new ViewIdViewModel());
            var claims = (ClaimsIdentity)User.Identity;
            var id = Convert.ToInt32(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(new ViewIdViewModel() { UserId = id });
        }

        //[Authorize]
        [HttpPost]
        public ActionResult GetContent(int? id, int? folderId)
        {
            var callerId = (User.Identity.IsAuthenticated ?
                    int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                    (int?)null);
           
            var objectsUnmapped = _service.GetObjects(callerId, id, folderId);
            var objects = _mapper.Map<ObjectsViewModel>(objectsUnmapped);
            if (objects != null)
            {
                //userId это id, чьи файлы мы просматриваем
                Session["userId"] = id;
                Session["parentId"] = objects.ParentFolderId;
            }
            return PartialView(objects);
        }
        [Authorize]
        [HttpPost]
        public ActionResult GetFileInfo(int id)
        {
            var callerId = (User.Identity.IsAuthenticated ?
                    int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                    (int?)null);
            var fileInfo = _mapper.Map<FileInfoViewModel>(_service.GetFileInfo(callerId, id));
            return PartialView("FileInfo", fileInfo);
        }

        [HttpPost]
        public ActionResult GetFolderInfo(int id)
        {
            var callerId = (User.Identity.IsAuthenticated ?
                    int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                    (int?)null);
            int? sessionId = (int?)Session["userId"];
            var folderInfo = _service.GetFolderInfo(callerId, id);
            var viewInfo = _mapper.Map<FolderInfoViewModel>(folderInfo);
            if (sessionId != callerId)
                viewInfo.ReadOnlyLink = true;
            return PartialView("FolderInfo", viewInfo);
        }
        [Authorize]
        [HttpPost]
        [SameCallerAsRequired]
        public ActionResult ChangeAccess(int objectId,FileAccess level,bool isFile)
        {
            _service.ChangeAccess(objectId, level, isFile);
            return Json(Console.In);
        }
        [Authorize]
        [HttpPost]
        [SameCallerAsRequired]
        public ActionResult AddFolder(string name)
        {
            int? fromSessionId = (int?)Session["userId"];
            if (!User.Identity.IsAuthenticated || !fromSessionId.HasValue)
                return new HttpUnauthorizedResult("Неавторизованным пользователям запрещено добавлять папки");
            
            if (Session["parentId"] == null)
                throw new HttpException();
            int parentFolderId = (int)Session["parentId"];
            int newId = _service.AddFolder(fromSessionId.Value, parentFolderId, name);
            if (newId > 0)
                return Json(new { status = true, id = newId }, JsonRequestBehavior.AllowGet);
            return Json(new { status = false}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        [SameCallerAsRequired]
        public ActionResult UploadFiles(int? folderId, IEnumerable<HttpPostedFileBase> files)
        {
            int callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);

            _service.UploadFiles(callerId, folderId.HasValue ? folderId : (int?)Session["parentId"], _mapper.Map<IEnumerable<FileUploadDTO>>(files));
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize]
        [SameCallerAsRequired]
        public ActionResult MakeLink(int objectId, string link, bool isFile = true)
        {
            if (!_service.TryCreateLink(objectId, link, isFile, out string error))
                return Json(new { status = false, errorMessage = error });
            return Json(new { link, status = true  });
        }
        [HttpPost]
        [Authorize]
        [SameCallerAsRequired]
        public ActionResult RemoveFolder(int id)
        {
            int callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);

            _service.RemoveFolder(callerId, id);
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult DownloadFile(int fileId)
        {
            int? userId = (int?)Session["userId"];
            int? callerId = User.Identity.IsAuthenticated ?
                int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                (int?)null;
            string randUrl = _urlGenerator.GetSalt(32);
            TempData[randUrl] = new int?[] { callerId, userId, fileId }; 
            return Json(new { url = "/download/" + randUrl });
        }

        public ActionResult DownloadFile(string fileUrl)
        {
            int? callerId;
            int userId, fileId;
            var vals = (int?[])TempData[fileUrl];
            if (TempData[fileUrl] != null)
            {
                callerId = vals[0];
                userId = vals[1].Value;
                fileId = vals[2].Value;
                TempData.Remove(fileUrl);
            } else
                return new HttpNotFoundResult();

            var file = _service.DownloadFile(callerId, userId, fileId);
            if (file.IsPermitted)
                return File(file.Data, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
            else
                return new HttpUnauthorizedResult();
        }
        
        [Authorize]
        [HttpPost]
        [SameCallerAsRequired]
        public ActionResult RemoveFile(int id)
        {
            int callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
            _service.RemoveFile(callerId, id);
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
        }
    }
}