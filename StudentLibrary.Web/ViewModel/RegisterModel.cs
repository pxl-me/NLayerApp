using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentLibrary.Web.ViewModel
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public Group Group { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Pasword does not match.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }

    public enum Group
    {
        IS81,
        IS82,
        IS83
    }
}