using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public interface IPlanSqlDAL
    {
        int CreatePlan(string planName);

        bool AddUserAndPlanID(int planID, int userID);

        List<PlanModel> GetAllUserPlans(int userID);

        int CreateMeal(string dayOfWeek, string mealtype);

        string GetPlanName(int planID);

        PlanModel GetMeal(int mealID);


        bool AddMealAndPlanID(int mealID, int planID);

        bool InsertMealAndRecipeID(int mealID, int recipeID);

        List<int> GetAllMealsInPlan(int planid);


        int GetRecipeFromMeal(int mealid);



        //this can get recipe data from a list of mealIDs
        List<RecipeModel> GetAllRecipesFromMeals(List<int> mealIDs);


        List<PlanModel> GetPlan(int planID);

    }


  
}
