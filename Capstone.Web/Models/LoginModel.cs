using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Input Was Invalid")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Input Was Invalid")]
        public string Password { get; set; }

        public string Salt { get; set; }
    }
}