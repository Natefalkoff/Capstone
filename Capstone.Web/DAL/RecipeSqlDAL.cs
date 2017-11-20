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
        //private int recipeId = 0;
        private readonly string connectionString;
        private string insertRecipe = @"INSERT INTO recipe(recipe_name, directions, ingredients, image_name, publics) VALUES ( @recipe_name, @directions, @ingredients, @image_name, @publics);SELECT CAST(scope_identity() AS int);";
        private string insertTagsId = @"INSERT INTO recipe_tags VALUES (@recipeId, @tagId);";
        private string getRecipes = @"SELECT * FROM recipe;";
        //private string tagExists = @"SELECT COUNT(*) FROM tags WHERE tag_name = @tagExists;";
        private string getTagId = @"SELECT tag_id FROM tags WHERE tag_name = @tagIfExists;";
        private string getTagIdAfterInsert = @"INSERT INTO tags OUTPUT INSERTED.tag_id VALUES (@tag_name);";
        private string recipeDetails = @"SELECT * FROM recipe LEFT OUTER JOIN recipe_category ON recipe.recipe_id = recipe_category.recipe_id LEFT OUTER JOIN category.category_id = recipe_category.category_id LEFT OUTER JOIN  WHERE recipe_id = @recipe_id;";

        //using for details , does not include tags / categories
        private string recipeDetailSql = @"SELECT * FROM recipe WHERE recipe_id = @recipe_id";

        public RecipeSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool InsertRecipeIdAndTagId(int recipeId, int tagId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertTagsId, conn);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
        public int GetTagIdAfterInsert(string tag)
        {
            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getTagIdAfterInsert, conn);
                    cmd.Parameters.AddWithValue("@tag_name", tag);
                    id = (int)cmd.ExecuteScalar();
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return id;
        }

        public int GetTagIdIfExists(string tag)
        {
            int id = 0;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getTagId, conn);
                    cmd.Parameters.AddWithValue("@tagIfExists", tag);
                    id = (int)cmd.ExecuteScalar();
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return id;
        }

        public List<int> TagsExist(string tags)
        {
            List<int> exists = new List<int>();
            string[] splitTags = tags.Split(' ');
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    for(int i = 0; i < splitTags.Length; i++)
                    {
                        string tagExists = string.Format(@"SELECT COUNT(*) FROM tags WHERE tag_name = @tagExists{0};", i);
                        SqlCommand cmd = new SqlCommand(tagExists, conn);
                        cmd.Parameters.AddWithValue(string.Format("@tagExists{0}", i), splitTags[i]);
                        int id = (int)cmd.ExecuteScalar();
                        exists.Add(id);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return exists;
        }

        public int NewRecipe(RecipeModel recipe)
        {
            int recipeId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                    cmd.Parameters.AddWithValue("@directions", recipe.Directions);
                    cmd.Parameters.AddWithValue("@ingredients", recipe.Ingredients);
                    cmd.Parameters.AddWithValue("@image_name", recipe.ImageName);
                    cmd.Parameters.AddWithValue("@publics", recipe.Publics);
                    recipeId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return recipeId;
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
                    SqlCommand cmd = new SqlCommand(recipeDetailSql, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", id);
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
