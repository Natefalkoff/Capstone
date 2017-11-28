using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class PlanSqlDAL : IPlanSqlDAL
    {
        private readonly string connectionString;
        private string createPlanSql = @"INSERT INTO plans(plan_name) VALUES ( @plan_name);SELECT CAST(scope_identity() AS int);";
        private string GetPlanSql = @"select plans.plan_name, meal.meal_name, meal.day_of_week, meal_recipe.recipe_id 
                                    FROM plans
                                    JOIN user_plan ON plans.plan_id = user_plan.plan_id
                                    JOIN meal_plan ON plans.plan_id = meal_plan.plan_id
                                    JOIN meal ON meal.meal_id = meal_plan.meal_id
                                    JOIN meal_recipe ON meal_recipe.meal_id = meal.meal_id
                                    WHERE plans.plan_id = @plan_id";

        private string addToUserPlanSql = "INSERT INTO user_plan (plan_id, users_id) VALUES (@planid, @userid)";
        private string getAllUserUserPlansSql = "select * from user_plan WHERE users_id = @userid";

        private string CreateMealSql = "INSERT INTO meal (day_of_week, meal_name) VALUES (@dayofweek, @mealname); SELECT CAST(scope_identity() AS int)";
        private string addMealAndPlanID= "INSERT INTO meal_plan (plan_id, meal_id) VALUES (@planid, @mealid)";
        private string addMealAndRecipeIDSql = "INSERT INTO meal_recipe (meal_id, recipe_id) VALUES (@mealid, @recipeid)";

        private string GetAllMealsInPlanSql = "select meal_id from meal_plan where plan_id = @planid";
        private string GetRecipeFromMealSql = "select recipe_id from meal_recipe where meal_id = @mealid";
        private string GetMealSql = "select day_of_week, meal_name from meal where meal_id = @mealid";
        private string GetPlanNameSql = "select plan_name from plans WHERE plan_id = @planid";


        private string DeleteMealPlanSql = "DELETE FROM meal_plan WHERE meal_id = @mealid";
        private string DeleteMealRecipeSql = "DELETE FROM meal_recipe WHERE meal_id = @mealid";
        private string DeleteMealSql = "Delete FROM meal WHERE meal_id = @mealid";

        public PlanSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string GetPlanName (int planID)
        {
            string result = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetPlanNameSql, conn);
                    cmd.Parameters.AddWithValue("@planid", planID);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {

                       result = Convert.ToString(results["plan_name"]);
                       
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }

        public int CreatePlan (string planName)
        {
            int planId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(createPlanSql, conn);
                    cmd.Parameters.AddWithValue("@plan_name", planName);
                    planId = (int)cmd.ExecuteScalar();
                }
                return planId;
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public bool AddUserAndPlanID (int planID, int userID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(addToUserPlanSql, conn);
                    cmd.Parameters.AddWithValue("@planid", planID);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<PlanModel> GetAllUserPlans (int userID)
        {
            List<PlanModel> result = new List<PlanModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getAllUserUserPlansSql, conn);
                    cmd.Parameters.AddWithValue("@userid", userID);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        PlanModel p = new PlanModel();
                        p.PlanId = Int32.Parse((results["plan_id"].ToString()));
                        result.Add(p);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }

        public int CreateMeal (string dayOfWeek, string mealtype)
        {
            int mealId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(CreateMealSql, conn);
                    cmd.Parameters.AddWithValue("@dayofweek", dayOfWeek);
                    cmd.Parameters.AddWithValue("@mealname", mealtype);
                    mealId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return mealId;

        }

        //done ??? code review
        public PlanModel GetMeal (int mealID)
        {
            PlanModel p = new PlanModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetMealSql, conn);
                    cmd.Parameters.AddWithValue("@mealid", mealID);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                      
                        p.Meal = Convert.ToString(results["meal_name"]);
                        p.Day = Convert.ToString(results["day_of_week"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return p;

        }

        public bool AddMealAndPlanID (int mealID, int planID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(addMealAndPlanID, conn);
                    cmd.Parameters.AddWithValue("@planid", planID);
                    cmd.Parameters.AddWithValue("@mealid", mealID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool InsertMealAndRecipeID (int mealID, int recipeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(addMealAndRecipeIDSql, conn);
                    cmd.Parameters.AddWithValue("@mealid", mealID);
                    cmd.Parameters.AddWithValue("@recipeid", recipeID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<int> GetAllMealsInPlan(int planid)
        {
            List<int> result = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllMealsInPlanSql, conn);
                    cmd.Parameters.AddWithValue("@planid", planid);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        int p;
                        p = Int32.Parse((results["meal_id"].ToString()));
                        result.Add(p);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }

        public int GetRecipeFromMeal(int mealid)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetRecipeFromMealSql, conn);
                    cmd.Parameters.AddWithValue("@mealid", mealid);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        
                        result = Int32.Parse((results["recipe_id"].ToString()));
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result;
        }

        //this can get recipe data from a list of mealIDs
        public List<RecipeModel> GetAllRecipesFromMeals (List<int> mealIDs)
        {
          
            List<int> Recipes = new List<int>();
            foreach (int mealID in mealIDs)
            {
                int rec;
                rec = this.GetRecipeFromMeal(mealID);
                Recipes.Add(rec);
            }

            List<RecipeModel> AllRecipes = new List<RecipeModel>();
            foreach (int recipe in Recipes)
            {
                RecipeSqlDAL recipeDal = new RecipeSqlDAL(connectionString);
                RecipeModel thisRec = new RecipeModel();
                thisRec = recipeDal.RecipeDetail(recipe);
                AllRecipes.Add(thisRec);
            }

            return AllRecipes;
        }

        public List<PlanModel> GetPlan (int planID)
        {
            List<int> allMeals = this.GetAllMealsInPlan(planID);
            List<PlanModel> result = new List<PlanModel>();
            string planName = this.GetPlanName(planID);
            foreach (int mealid in allMeals)
            {
                PlanModel P = new PlanModel();
                P = this.GetMeal(mealid);
                P.PlanName = planName;
                P.MealId = mealid;
                result.Add(P);
            }

            List<RecipeModel> allRecipes = this.GetAllRecipesFromMeals(allMeals);

            for (int i = 0; i < allMeals.Count; i++)
            {
                result[i].Recipes = allRecipes[i];
            }

            return result;


        }

        public bool DeleteMeal(int planID, int mealID)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(DeleteMealPlanSql, conn);
                SqlCommand cmd1 = new SqlCommand(DeleteMealRecipeSql, conn);
                SqlCommand cmd2 = new SqlCommand(DeleteMealSql, conn);
                cmd.Parameters.AddWithValue("@mealid", mealID);
                cmd1.Parameters.AddWithValue("@mealid", mealID);
                cmd2.Parameters.AddWithValue("@mealid", mealID);
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                int rows = cmd2.ExecuteNonQuery();


                return rows > 0;
            }
        }
    }
}