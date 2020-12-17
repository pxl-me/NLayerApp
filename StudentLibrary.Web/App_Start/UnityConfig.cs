using StudentLibrary.Interfaces.Models;
using StudentLibrary.Interfaces.Repositories;
using StudentLibrary.Interfaces.Services;
using StudentLibrary.Models;
using StudentLibrary.Repositories;
using StudentLibrary.Services;
using StudentLibrary.Web.Interfaces;
using StudentLibrary.Web.Interfaces.Repositories;
using StudentLibrary.Web.Interfaces.Services;
using StudentLibrary.Web.Models;
using StudentLibrary.Web.Repositories;
using StudentLibrary.Web.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace StudentLibrary.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<StudentLibrary.Interfaces.Repositories.IRepository<Document>, DocumentRepository>();
            container.RegisterType<IDocumentService, DocumentService>();
            container.RegisterType<IContext, LibraryContext>();
            container.RegisterType<IUnitRepository, UnitRepository>();
            container.RegisterType<IRepository, UserRepository>();
            container.RegisterType<IUserService, UserService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}