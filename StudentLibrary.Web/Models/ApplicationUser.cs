using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentLibrary.Web.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }

        [DefaultValue(0)]
        public int BookCount { get; set; }
        public ApplicationUser()
        {

        }
    }
}