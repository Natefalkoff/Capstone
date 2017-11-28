using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class SearchSqlDAL : ISearchSqlDAL
    {
        private readonly string connectionString;
        SearchModel model = new SearchModel();

        public SearchSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<RecipeModel> GetSearchResults(SearchModel model)
        {
            List<string> tagStrings = new List<string>();
            List<RecipeModel> results = new List<RecipeModel>();
            if (model.TagSearch != null)
            {
                string[] array = model.TagSearch.Split(';');
                foreach(string s in array)
                {
                    tagStrings.Add(s.TrimStart(' ').ToLower());
                }
            }
            List<string> list = new List<string>();
            foreach(KeyValuePair<string, bool> kvp in model.SearchCategories)
            {
                if(kvp.Value == true)
                {
                    list.Add(kvp.Key);
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if(list.Count > 0 && model.TagSearch == null)
                    {
                        for(int i = 0; i < list.Count; i++)
                        {
                            conn.Open();
                            
                            string categoryResults = string.Format(@"SELECT * FROM recipe JOIN recipe_category ON recipe.recipe_id = recipe_category.recipe_id JOIN category ON recipe_category.category_id = category.category_id WHERE category_name = @catName{0} AND approved = 1 AND publics = 1;", i);
                            SqlCommand cmd = new SqlCommand(categoryResults, conn);
                            cmd.Parameters.AddWithValue(string.Format("@catName{0}", i), list[i]);
                            SqlDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                RecipeModel r = new RecipeModel();
                                r.Name = Convert.ToString(read["recipe_name"]);
                                r.Directions = Convert.ToString(read["directions"]);
                                r.ImageName = Convert.ToString(read["image_name"]);
                                r.Ingredients = Convert.ToString(read["ingredients"]);
                                r.RecipeID = Convert.ToInt32(read["recipe_id"]);
                                results.Add(r);
                            }
                            conn.Close();
                        }
                    }

                    if (tagStrings.Count > 0 && list.Count == 0)
                    {
                        if (tagStrings.Count > 0)
                        {
                            for (int i = 0; i < tagStrings.Count; i++)
                            {
                                conn.Open();

                                string tagResults = string.Format(@"SELECT * FROM recipe JOIN recipe_tags ON recipe.recipe_id = recipe_tags.recipe_id JOIN tags ON recipe_tags.tag_id = tags.tag_id WHERE tag_name = @tagName{0} AND approved = 1 AND publics = 1;", i);
                                SqlCommand cmd = new SqlCommand(tagResults, conn);
                                cmd.Parameters.AddWithValue(string.Format("@tagName{0}", i), tagStrings[i]);
                                SqlDataReader read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    RecipeModel r = new RecipeModel();
                                    r.Name = Convert.ToString(read["recipe_name"]);
                                    r.Directions = Convert.ToString(read["directions"]);
                                    r.ImageName = Convert.ToString(read["image_name"]);
                                    r.Ingredients = Convert.ToString(read["ingredients"]);
                                    r.RecipeID = Convert.ToInt32(read["recipe_id"]);
                                    results.Add(r);
                                }
                                conn.Close();
                            }
                        }
                    }
                        if(tagStrings.Count > 0 && list.Count > 0)
                        {
                            for (int i = 0; i < tagStrings.Count; i++)
                            {
                                for (int j = 0; j < list.Count; j++)
                                {
                                    conn.Open();

                                    string tagResults = string.Format(@"SELECT * FROM recipe JOIN recipe_tags ON recipe.recipe_id = recipe_tags.recipe_id JOIN tags ON recipe_tags.tag_id = tags.tag_id JOIN recipe_category ON recipe_category.recipe_id = recipe.recipe_id JOIN category ON category.category_id = recipe_category.category_id WHERE tag_name = @tagName{0} AND category_name = ", i);
                                    string catResults = string.Format(@"@catName{0} AND approved = 1 AND publics = 1;", j);
                                    tagResults = tagResults + catResults;
                                    SqlCommand cmd = new SqlCommand(tagResults, conn);
                                    cmd.Parameters.AddWithValue(string.Format("@tagName{0}", i), tagStrings[i]);
                                    cmd.Parameters.AddWithValue(string.Format("@catName{0}", j), list[j]);
                                    SqlDataReader read = cmd.ExecuteReader();
                                    while (read.Read())
                                    {
                                        RecipeModel r = new RecipeModel();
                                        r.Name = Convert.ToString(read["recipe_name"]);
                                        r.Directions = Convert.ToString(read["directions"]);
                                        r.ImageName = Convert.ToString(read["image_name"]);
                                        r.Ingredients = Convert.ToString(read["ingredients"]);
                                        r.RecipeID = Convert.ToInt32(read["recipe_id"]);
                                        results.Add(r);
                                    }
                                    conn.Close();
                                }
                            }
                        }
                    }
                
            }
            catch(SqlException ex)
            {
                throw;
            }
            List<RecipeModel> distinctList = results.GroupBy(i => i.RecipeID).Select(g => g.First()).ToList();
            return distinctList;
            
        }
    }
}