using ActualFileStorage.BLL.Links;
using ActualFileStorage.DAL.Models;
using ActualFileStorage.DAL.UOW;
using ActualFileStorage.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActualFileStorage.DAL.Adapters;
using ActualFileStorage.DAL.Repositories;

namespace ActualFileStorage.PL.Controllers
{
    public class ShortenerController : Controller
    {
        private ILinkResolver _link;
        private IUnitOfWork _uow;
        public ShortenerController(ILinkResolver resolver)//, IUnitOfWork unitOfWork)
        {
            System.IO.File.AppendAllLines(Properties.Resources.logfile, new[] { $"Constructor of {GetType()} controller invoked" });
            _link = resolver;
            //_uow = unitOfWork;
        }
        // GET: Shortener
        public ActionResult Unpack(object id)
        {
            Console.WriteLine("{0} {1}", _link.Decode(id.ToString()), _link.Encode(_link.Decode(id.ToString())));
            Console.WriteLine($"Received {id}");

            SimpleViewModel svm = new SimpleViewModel() {
                id = id
            };
            using (IUnitOfWork uow = new BasicUOW(new EFAdapter(new FileStorageContext())))
            {
                IRepository<Folder> repo = uow.GetRepo<Folder>();
                //(repo as Repository<Folder>).ChangeType<Folder>();
                svm.single = repo.GetById(1);
                svm.hello = repo.GetAll().ToList();
            }
            return View(svm);
        }

        public class SimpleViewModel
        {
            public object id;
            public IEnumerable<Folder> hello;
            public Folder single;
        }
    }
}