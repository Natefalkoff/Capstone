using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public interface IRecipeSqlDAL
    {
        bool NewRecipe(RecipeModel recipe);
        List<RecipeModel> GetRecipes();
        RecipeModel RecipeDetail(int id);
    }
}
