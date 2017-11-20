﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IRecipeSqlDAL
    {
       int NewRecipe(RecipeModel recipe);
       List<RecipeModel> GetRecipes();
       RecipeModel RecipeDetail(int id);
        bool InsertRecipeIdAndTagId(int recipeId, int tagId);
        int GetTagIdAfterInsert(string tag);
        int GetTagIdIfExists(string tag);
        List<int> TagsExist(string tags);
    }
}
