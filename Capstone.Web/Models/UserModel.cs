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

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Does not match email")]
        public string ConfirmEmail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Does not match password")]
        public string ConfirmPassword { get; set; }

        public int? AuthorizationLevel = 1;
        public List<RecipeModel> Recipes { get; set; }
        public List<RecipeModel> Plan { get; set; }
        public string Salt { get; set; }

        public string TempPassword { get; set; }

        public int Signup { get; set; }
        public bool SignupBool { get; set; }
        
    }
}