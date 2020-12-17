using StudentLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLibrary.Web.Interfaces.Services
{
    public interface IUserService
    {
        List<ApplicationUser> GetApplicationUsers();
        ApplicationUser GetUserById(string Id);
        void Edit(ApplicationUser newUser);
        void Delete(ApplicationUser user);
        List<ApplicationUser> SearchUser(string searchString);

        ApplicationUser GetAdmin();
    }
}