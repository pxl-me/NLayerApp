using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentLibrary.Web.Models
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    { 
        public ApplicationContext() : base("DevConnection") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

      
    }
}