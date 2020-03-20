using ActualFileStorage.BLL.DTOs;
using ActualFileStorage.BLL.Salts;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public ProfileController(IProfileService service, ISaltBuilder urlGenerator)//, IMapper mapper)
        {
            _service = service;
            //_mapper = mapper;
            _urlGenerator = urlGenerator;
        }
        // GET: Profile
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return View(model: "null");
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(model: id);
        }
        [Authorize]
        [HttpPost]
        public ActionResult GetContent(int? id, int? folderId, IEnumerable<HistoryItemViewModel> history)
        {
            //userId это id, чьи файлы мы просматриваем
            Session["userId"] = id;
            //Session["parentId"] = folderId ?? _service.GetRootFolderIdByUserId(int );
            var callerId = (User.Identity.IsAuthenticated ?
                    int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                    (int?)null);
            //var userId = id.HasValue ? id.Value : (int?) null;
            var objectsUnmapped = _service.GetObjects(callerId, id, folderId, _mapper.Map<IEnumerable<HistoryItemDTO>>(history));
            var objects = _mapper.Map<ObjectsViewModel>(objectsUnmapped);
            Session["parentId"] = objects.ParentFolderId;
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
        [Authorize]
        [HttpPost]
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
        [Authorize]
        [HttpPost]
        [SameCallerAsRequired]
        public ActionResult UploadFiles(int? folderId, IEnumerable<HttpPostedFileBase> files)
        {
            //int? fromSessionId = (int?)Session["userId"];
            //if (!User.Identity.IsAuthenticated || !fromSessionId.HasValue)
            //    return new HttpUnauthorizedResult("Неавторизованным пользователям запрещено загружать файлы");
            //var callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
            
            //if (callerId != fromSessionId)
            //    return new HttpUnauthorizedResult("Запрещено загружать файлы другим пользователям");
            
            int callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);

            _service.UploadFiles(callerId, folderId.HasValue ? folderId : (int?)Session["parentId"], _mapper.Map<IEnumerable<FileUploadDTO>>(files));
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [SameCallerAsRequired]
        public ActionResult RemoveFolder(int id)
        {
            //int? fromSessionId = (int?)Session["userId"];
            //if (!User.Identity.IsAuthenticated || !fromSessionId.HasValue)
            //    return new HttpUnauthorizedResult("Неавторизованным пользователям запрещено удалять папки");
            //var callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
            //if (callerId != fromSessionId)
            //    return new HttpUnauthorizedResult("Запрещено удалять папки другим пользователям");
            int callerId = int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);

            _service.RemoveFolder(callerId, id);
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
        }

        //[Authorize]
        [HttpPost]
        public ActionResult DownloadFile(int fileId)
        {
            int userId = (int)Session["userId"];
            int? callerId = User.Identity.IsAuthenticated ?
                int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                (int?)null;
            string randUrl = _urlGenerator.GetSalt(32);
            TempData[randUrl] = new int?[] { callerId, userId, fileId }; //_service.DownloadFile(callerId, userId, fileId);
            //var file = _service.DownloadFile(callerId, userId, fileId);
            return Json(new { url = "/download/" + randUrl });//File(file.Data, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
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
            //Response.Headers
            //Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            //int userId = (int)Session["userId"];
            //int? callerId = User.Identity.IsAuthenticated ?
            //    int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
            //    (int?)null;
            //string randUrl = _urlGenerator.GetSalt(32);
            ////TempData[randUrl] = _service.DownloadFile(callerId, userId, fileId);
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