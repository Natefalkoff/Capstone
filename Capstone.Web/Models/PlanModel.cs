using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class PlanModel
    {
        private List<string> days;
        public List<string> Days
        {
            get { return this.days; }
            set
            {
                this.days = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            }
        }
        private List<string> meals;
        public List<string> Meals
        {
            get { return this.meals; }
            set
            {
                this.meals = new List<string> { "Breakfast", "Lunch", "Dinner", "Snack" };
            }
        }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public string Meal { get; set; }
        public string Day { get; set; }
        public RecipeModel Recipes { get; set; }
        public string PlanName { get; set; }

    }
}