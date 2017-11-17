using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserModelcs
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RecipeModel> Recipes { get; set; }
        public List<RecipeModel> Plan { get; set; }
    }
}