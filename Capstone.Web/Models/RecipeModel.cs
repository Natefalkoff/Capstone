using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class RecipeModel
    {
        public string Name {get; set;}
        public string Directions { get; set; }
        public int UserID { get; set; }
        public string Ingredients { get; set; }
        public string ImageName { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Categories { get; set; }

            

    }
}