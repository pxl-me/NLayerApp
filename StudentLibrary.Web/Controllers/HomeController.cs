using PagedList;
using StudentLibrary.Interfaces.Services;
using StudentLibrary.Web.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentLibrary.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        IDocumentService documentService;
        IUserService userService;

        public HomeController(IDocumentService documentService, IUserService userService)
        {
            this.documentService = documentService;
            this.userService = userService;
        }
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "author" ? "author_desc" : "author";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var documents = documentService.GetAllDocuments().FindAll(x=>x.UserName=="admin");

            if (!String.IsNullOrEmpty(searchString))
            {
                documents = documentService.SearchDoc(searchString);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    documents = documents.OrderByDescending(x => x.Title).ToList();
                    break;
                case "author":
                    documents = documents.OrderBy(x => x.Author).ToList();
                    break;
                case "author_desc":
                    documents = documents.OrderByDescending(x => x.Author).ToList();
                    break;
                default:
                    documents = documents.OrderBy(x => x.Title).ToList();
                    break;
            }

            var user = userService.GetApplicationUsers().ToList().Find(x => x.UserName == User.Identity.Name);
            if (User.Identity.Name == "admin")
            {
                user = userService.GetAdmin();
            }
            
            ViewBag.BookCount = user.BookCount;
            if (user.BookCount == 4)
            {
                ViewBag.Check = "You can't take more than 4 documents.";
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(documents.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetMyDoc(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "author" ? "author_desc" : "author";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var documents = documentService.GetAllDocuments().FindAll(x => x.UserName == User.Identity.Name);
            if (!String.IsNullOrEmpty(searchString))
            {
                documents = documentService.SearchDoc(searchString);
            }

            switch (sortOrder)
            {
                case "title_desc":
                    documents = documents.OrderByDescending(x => x.Title).ToList();
                    break;
                case "author":
                    documents = documents.OrderBy(x => x.Author).ToList();
                    break;
                case "author_desc":
                    documents = documents.OrderByDescending(x => x.Author).ToList();
                    break;
                default:
                    documents = documents.OrderBy(x => x.Title).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(documents.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id)
        {
            var document = documentService.GetDocumentById(id);
            return View(document);
        }

        [HttpPost]
        public void TakeDoc(int id)
        {
            var document = documentService.GetDocumentById(id);
            var user = userService.GetApplicationUsers().ToList().Find(x => x.UserName == User.Identity.Name);
            if (user.BookCount < 4)
            {
                user.BookCount = user.BookCount + 1;
                document.UserName = User.Identity.Name;
                documentService.Update(document);
                userService.Edit(user);
            }
            else
            {
                ViewBag.Check = "You can't take more than 4 documents.";
            }
            ViewBag.BookCount = user.BookCount;
        }

        [HttpPost]
        public void ReturnDoc(int id)
        {
            var document = documentService.GetDocumentById(id);
            var user = userService.GetApplicationUsers().ToList().Find(x => x.UserName == User.Identity.Name);
            user.BookCount = user.BookCount - 1;
            document.UserName = "admin";
            documentService.Update(document);
            userService.Edit(user);
        }


    }
}