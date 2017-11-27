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
        private string getCatsFromId = @"SELECT * FROM recipe JOIN recipe_category ON recipe.recipe_id = recipe_category.recipe_id JOIN category ON recipe_category.category_id = category.category_id WHERE recipe.recipe_id = @id;";
        private string getTagsFromId = @"SELECT * FROM recipe Join recipe_tags ON recipe.recipe_id = recipe_tags.recipe_id Join tags on tags.tag_id = recipe_tags.tag_id where recipe.recipe_id = @id;";
        private string insertRecipe = @"INSERT INTO recipe(recipe_name, directions, ingredients, image_name, publics) VALUES ( @recipe_name, @directions, @ingredients, @image_name, @publics);SELECT CAST(scope_identity() AS int);";
        private string insertTagsId = @"INSERT INTO recipe_tags VALUES (@recipeId, @tagId);";
        private string getRecipes = @"SELECT * FROM recipe;";
        private string getAllPublicRecipes = @"SELECT * FROM recipe WHERE approved = 1 AND publics = 1;";
        private string getAllNonApprovedRecipes = @"SELECT * FROM recipe JOIN user_recipes ON recipe.recipe_id = user_recipes.recipe_id JOIN website_users ON user_recipes.users_id = website_users.users_id WHERE approved = 0 AND publics = 1;";
        //private string tagExists = @"SELECT COUNT(*) FROM tags WHERE tag_name = @tagExists;";
        private string getTagId = @"SELECT tag_id FROM tags WHERE tag_name = @tagIfExists;";
        private string getTagIdAfterInsert = @"INSERT INTO tags OUTPUT INSERTED.tag_id VALUES (@tag_name);";
        private string recipeDetails = @"SELECT * FROM recipe LEFT OUTER JOIN recipe_category ON recipe.recipe_id = recipe_category.recipe_id LEFT OUTER JOIN category.category_id = recipe_category.category_id LEFT OUTER JOIN  WHERE recipe_id = @recipe_id;";
        private string getAllCategories = @"SELECT category_name FROM category";
        private string getCategoryId = @"SELECT category_id FROM category WHERE category_name = @getCategoryId;";
        private string insertCategoryIdAndRecipeId = @"INSERT INTO recipe_category(recipe_id, category_id) VALUES (@recipeCategoryId, @categoryRecipeId);";
        private string updateApproval = @"UPDATE recipe SET approved = 1 WHERE recipe_id = @recipeId;";
        private string insertRecipeIdandUserId = @"INSERT INTO user_recipes VALUES ( @userId, @recipeId);";
        

        //using for details , does not include tags / categories
        private string recipeDetailSql = @"SELECT * FROM recipe WHERE recipe_id = @recipe_id";
        

        public RecipeSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GetCatsFromId(int id)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getCatsFromId, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        list.Add(Convert.ToString(results["category_name"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return list;
        }

        public List<string> GetTagsFromId(int id)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getTagsFromId, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        list.Add(Convert.ToString(results["tag_name"]));
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return list;
        }

        public bool InsertRecipeIdAndUserId(int userId, int recipeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertRecipeIdandUserId, conn);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public bool UpdateApproval(int recipeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(updateApproval, conn);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
        public bool InsertRecipeAndCategoryId(int recipeId, int categoryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertCategoryIdAndRecipeId, conn);
                    cmd.Parameters.AddWithValue("@recipeCategoryId", recipeId);
                    cmd.Parameters.AddWithValue("@categoryRecipeId", categoryId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }

            catch(SqlException ex)
            {
                throw;
            }
        }
        public List<RecipeModel> GetPublicNonApprovedRecipes()
        {
            List<RecipeModel> recipes = new List<RecipeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getAllNonApprovedRecipes, conn);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        RecipeModel r = new RecipeModel();
                        r.Name = Convert.ToString(results["recipe_name"]);
                        r.Directions = Convert.ToString(results["directions"]).Replace("\\n", "\n");
                        r.ImageName = Convert.ToString(results["image_name"]);
                        r.Ingredients = Convert.ToString(results["ingredients"]).Replace("\\n", "\n");
                        r.RecipeID = Convert.ToInt32(results["recipe_id"]);
                        r.UserID = Convert.ToInt32(results["users_id"]);
                        r.UserName = Convert.ToString(results["users_name"]);
                        r.Approved = Convert.ToInt32(results["approved"]);
                        r.Publics = Convert.ToInt32(results["publics"]);
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
        public List<RecipeModel> GetPublicApprovedRecipes()
        {
            List<RecipeModel> recipes = new List<RecipeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getAllPublicRecipes, conn);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        RecipeModel r = new RecipeModel();
                        r.Name = Convert.ToString(results["recipe_name"]);
                        r.Directions = Convert.ToString(results["directions"]).Replace("\\n", "\n");
                        r.ImageName = Convert.ToString(results["image_name"]);
                        r.Ingredients = Convert.ToString(results["ingredients"]).Replace("\\n", "\n");
                        r.RecipeID = Convert.ToInt32(results["recipe_id"]);
                        r.Approved = Convert.ToInt32(results["approved"]);
                        r.Publics = Convert.ToInt32(results["publics"]);
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
        public int GetCategoryId(string cat)
        {
            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getCategoryId, conn);
                    cmd.Parameters.AddWithValue("@getCategoryId", cat);
                    id = (int)cmd.ExecuteScalar();
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return id;
        }
        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getAllCategories, conn);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        categories.Add(Convert.ToString(results["category_name"]));
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return categories;
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
            string[] splitTags = tags.Split(';');
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    for(int i = 0; i < splitTags.Length; i++)
                    {
                        string tagExists = string.Format(@"SELECT COUNT(*) FROM tags WHERE tag_name = @tagExists{0};", i);
                        SqlCommand cmd = new SqlCommand(tagExists, conn);
                        cmd.Parameters.AddWithValue(string.Format("@tagExists{0}", i), splitTags[i].TrimStart());
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
                        r.Directions = Convert.ToString(results["directions"]).Replace("\\n", "\n");
                        r.ImageName = Convert.ToString(results["image_name"]);
                        r.Ingredients = Convert.ToString(results["ingredients"]).Replace("\\n", "\n");
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
