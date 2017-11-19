using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int AuthorizationLevel = 1;
        public List<RecipeModel> Recipes { get; set; }
        public List<RecipeModel> Plan { get; set; }
        
    }
}