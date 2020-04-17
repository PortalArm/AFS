using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Repositories;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.PL.Models;
using AutoMapper;
using System.Security.Claims;

namespace ActualFileStorage.PL.Controllers
{
    public class ShortenerController : Controller
    {
        private IShortenerService _service;
        private IProfileService _profileService;
        private IMapper _mapper
        {
            get => _service.Mapper;
        }
        public ShortenerController(IShortenerService service, IProfileService profileService)
        {
            _service = service;
            _profileService = profileService;
        }
        public ActionResult Unpack(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("~/Views/Shared/Error.cshtml");

            var callerId = (User.Identity.IsAuthenticated ?
                int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value) :
                (int?)null);

            var objectFromService = _service.GetIdsByLink(callerId, id);
            Session["userId"] = objectFromService.UserId;
            if (objectFromService.IsFile)
            {
                var fileInfo = _mapper.Map<FileInfoViewModel>(_profileService.GetFileInfo(callerId, objectFromService.FileId.Value));
                if (fileInfo == null)
                    return View("~/Views/Shared/Error.cshtml");

                if (callerId != objectFromService.UserId)
                    fileInfo.ReadOnlyLink = true;

                return View("~/Views/Profile/FileInfo.cshtml", fileInfo);
            }
            if (!objectFromService.Exists)
                return View("~/Views/Shared/Error.cshtml");
            return View("~/Views/Profile/Index.cshtml", _mapper.Map<ViewIdViewModel>(objectFromService));
        }
    }
}