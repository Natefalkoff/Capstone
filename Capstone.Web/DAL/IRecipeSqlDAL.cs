using Capstone.Web.Models;
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
        List<string> GetCategories();
        int GetCategoryId(string cat);
        bool InsertRecipeAndCategoryId(int recipeId, int categoryId);
        List<RecipeModel> GetPublicNonApprovedRecipes();
        List<RecipeModel> GetPublicApprovedRecipes();
        bool UpdateApproval(int recipeId);
        bool InsertRecipeIdAndUserId(int userId, int recipeId);
        List<string> GetCatsFromId(int id);
        List<string> GetTagsFromId(int id);
        List<UserModel> SubscribedUsers();
        List<RecipeModel> GetUserRecipes(int id);
        bool AddImage(string imageName, int id);
        bool DeleteRecipe(int id);
    }
}
