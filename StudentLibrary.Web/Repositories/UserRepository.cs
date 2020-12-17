using StudentLibrary.Web.Interfaces.Repositories;
using StudentLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentLibrary.Web.Repositories
{
    public class UserRepository:IRepository
    {

        public UserRepository()
        {
            
        }

        public List<ApplicationUser> GetApplicationUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Users.ToList().FindAll(x=>x.UserName!="admin");
            }
        }

        public ApplicationUser GetUserById(string Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Users.ToList().Find(x => x.Id == Id);
            }
        }

        public void Edit(ApplicationUser newUser)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var oldUser = db.Users.ToList().Find(x => x.Id == newUser.Id);
                db.Users.Remove(oldUser);
                db.SaveChanges();
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void Delete(ApplicationUser user)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public List<ApplicationUser> SearchUser(string searchString)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users= db.Users.Where(x => x.Name != "admin");
                return users.Where(x => x.Name.Contains(searchString) || x.Surname.Contains(searchString)).ToList();
            }
        }

        public ApplicationUser GetAdmin()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                return db.Users.ToList().Find(x => x.UserName == "admin");
            }
        }
    }
}