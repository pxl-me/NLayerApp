using StudentLibrary.Web.Interfaces.Repositories;
using StudentLibrary.Web.Interfaces.Services;
using StudentLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLibrary.Web.Services
{
    public class UserService: IUserService
    {
        IRepository userRepository;

        public UserService(IRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Delete(ApplicationUser user)
        {
            userRepository.Delete(user);
        }

        public void Edit(ApplicationUser newUser)
        {
            userRepository.Edit(newUser);
        }

        public ApplicationUser GetAdmin()
        {
            return userRepository.GetAdmin();
        }

        public List<ApplicationUser> GetApplicationUsers()
        {
            return userRepository.GetApplicationUsers();
        }

        public ApplicationUser GetUserById(string Id)
        {
            return userRepository.GetUserById(Id);
        }

        public List<ApplicationUser> SearchUser(string searchString)
        {
            return userRepository.SearchUser(searchString);
        }
    }
}