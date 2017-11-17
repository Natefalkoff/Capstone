using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Capstone.Web.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RecipeModel> Recipes { get; set; }    
        public List<RecipeModel> Plan { get; set; }

      
    }
}