using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class SearchModel
    {
        public List<RecipeModel> Recipes { get; set; }
        public string TagSearch { get; set; }
        public Dictionary<string, bool> SearchCategories { get; set; }
        public List<RecipeModel> RecipeHash { get; set; }

    }
}