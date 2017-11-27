using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DropDownPlans
    {
        public List<PlanModel> PlanModels { get; set; }
        public List<RecipeModel> Recipes { get; set; }
        public List<string> Days { get; set; }
        public List<string> Meals { get; set; }
        public int currentID { get; set; }


        public int selectedRecipe {get; set;}
        public int selectedPlan { get; set; }
        public string selectedDay { get; set; }
        public string selectedMeal { get; set; }
    }
}