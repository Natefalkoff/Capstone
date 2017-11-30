using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "*")]
        public string TempPassword { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Password", ErrorMessage = "Does not match password")]
        public string ConfirmPassword { get; set; }
    }
}