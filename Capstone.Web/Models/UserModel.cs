using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Email", ErrorMessage = "Does not match email")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Password", ErrorMessage = "Does not match password")]
        public string ConfirmPassword { get; set; }

        public int? AuthorizationLevel = 1;
        public List<RecipeModel> Recipes { get; set; }
        public List<RecipeModel> Plan { get; set; }
        
    }
}