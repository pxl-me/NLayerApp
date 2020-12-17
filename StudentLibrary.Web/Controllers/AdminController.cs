using StudentLibrary.Interfaces.Services;
using StudentLibrary.Models;
using StudentLibrary.Web.Interfaces.Services;
using StudentLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace StudentLibrary.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IUserService userService;
        IDocumentService documentService;
        
        public AdminController(IUserService userService, IDocumentService documentService)
        {
            this.userService = userService;
            this.documentService = documentService;
        }
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int ?page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SurnameSortParm = sortOrder == "surname" ? "surname_desc" : "surname";
            ViewBag.GroupSortParm = sortOrder == "group" ? "group_desc" : "group";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = userService.GetApplicationUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = userService.SearchUser(searchString);
            }
           
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(x => x.Name).ToList();
                    break;
                case "surname":
                    users = users.OrderBy(x => x.Surname).ToList();
                    break;
                case "surname_desc":
                    users = users.OrderByDescending(x => x.Surname).ToList();
                    break;
                case "group":
                    users = users.OrderBy(x => x.Group).ToList();
                    break;
                case "group_desc":
                    users = users.OrderByDescending(x => x.Group).ToList();
                    break;
                default:
                    users = users.OrderBy(x => x.Name).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetDocuments(string sortOrder, string searchString, string currentFilter, int? page)
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

            var documents = documentService.GetAllDocuments();
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

        public ActionResult CreateDoc()
        {
            var model = new Document();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDoc(Document model)
        {
            model.UserName = User.Identity.Name;
            documentService.Create(model);
            return View("DetailsDoc", model);
        }

        public ActionResult EditDoc(int id)
        {
            var document = documentService.GetDocumentById(id);
            return View(document);
        }

        [HttpPost]
        public ActionResult EditDoc(Document model)
        {
            documentService.Update(model);
            return View("DetailsDoc", model);
        }

        public ActionResult DetailsDoc(int id)
        {
            var document = documentService.GetDocumentById(id);
            return View(document);
        }

        [HttpDelete]
        public void DeleteDoc(int id)
        {
            var document = documentService.GetDocumentById(id);
            documentService.Delete(document);
        }

        public ActionResult Details(string Id)
        {
            var user = userService.GetUserById(Id);
            return View(user);
        }

        public ActionResult Edit(string Id)
        {
            var user = userService.GetUserById(Id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser user)
        {
            userService.Edit(user);
            return View("Details", user);
        }

        [HttpDelete]
        public void Delete(string Id)
        {
            var user = userService.GetUserById(Id);
            userService.Delete(user);
        }
    }
}