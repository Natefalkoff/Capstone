using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class PlanModel
    {
        public PlanModel ()
        {
            this.Meals = new List<string> { "Breakfast", "Lunch", "Dinner", "Snack" };
            this.Days = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        }

        private List<string> days;
        public List<string> Days
        {
            get { return days; }
            set
            {
                this.days = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            }
        }
        private List<string> meals;
        public List<string> Meals
        {
            get { return meals; }
            set
            {
                this.meals = new List<string> { "Breakfast", "Lunch", "Dinner", "Snack" };
            }
        }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public int RecipeId { get; set; }
        public int MealId { get; set; }
        public string Meal { get; set; }
        public string Day { get; set; }
        public RecipeModel Recipes { get; set; }
        public string PlanName { get; set; }

        public List<RecipeModel> AllRecipes { get; set; }


    }
}