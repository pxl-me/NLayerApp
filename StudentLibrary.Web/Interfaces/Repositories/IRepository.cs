using StudentLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Web.Interfaces.Repositories
{
    public interface IRepository
    {
        List<ApplicationUser> GetApplicationUsers();
        ApplicationUser GetUserById(string Id);
        void Edit(ApplicationUser newUser);
        void Delete(ApplicationUser user);
        List<ApplicationUser> SearchUser(string searchString);
        ApplicationUser GetAdmin();
    }
}
