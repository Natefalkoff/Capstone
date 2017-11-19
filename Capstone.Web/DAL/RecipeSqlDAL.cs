using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class RecipeSqlDAL : IRecipeSqlDAL
    {
        private readonly string connectionString;
        private string insertRecipe = "@INSERT INTO recipe VALUES ( @recipe_name, @directions, @publics, @ingredients, @image_name)";
        private string getRecipes = @"SELECT * FROM recipe";
        private string recipeDetails = @"SELECT * FROM recipe LEFT OUTER JOIN recipe_category ON recipe.recipe_id = recipe_category.recipe_id LEFT OUTER JOIN category.category_id = recipe_category.category_id LEFT OUTER JOIN  WHERE recipe_id = @recipe_id;";

        public RecipeSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool NewRecipe(RecipeModel recipe)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                    cmd.Parameters.AddWithValue("@directions", recipe.Directions);
                    cmd.Parameters.AddWithValue("@publics", recipe.Publics);
                    cmd.Parameters.AddWithValue("@ingredients", recipe.Ingredients);
                    cmd.Parameters.AddWithValue("@image_name", recipe.ImageName);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public List<RecipeModel> GetRecipes()
        {
            List<RecipeModel> recipes = new List<RecipeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getRecipes, conn);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        RecipeModel r = new RecipeModel();
                        r.Name = Convert.ToString(results["recipe_name"]);
                        r.Directions = Convert.ToString(results["directions"]);
                        r.ImageName = Convert.ToString(results["image_name"]);
                        r.Ingredients = Convert.ToString(results["ingredients"]);
                        r.RecipeID = Convert.ToInt32(results["recipe_id"]);
                        recipes.Add(r);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return recipes;
        }
        public RecipeModel RecipeDetail(int id)
        {
            RecipeModel m = new RecipeModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getRecipes, conn);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        RecipeModel r = new RecipeModel();
                        m.Name = Convert.ToString(results["recipe_name"]);
                        m.Directions = Convert.ToString(results["directions"]);
                        m.ImageName = Convert.ToString(results["image_name"]);
                        m.Ingredients = Convert.ToString(results["ingredients"]);
                        m.RecipeID = Convert.ToInt32(results["recipe_id"]);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return m;
        }

    }
}
