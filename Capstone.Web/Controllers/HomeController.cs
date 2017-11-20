using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;

        public HomeController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
        }

        // GET: Home
        public ActionResult Index()
        {
            List<RecipeModel> recipes = new List<RecipeModel>();
            recipes = recipeDal.GetRecipes();
            return View(recipes);
        }

        public ActionResult AddRecipe()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddRecipe(RecipeModel recipe)
        {
            Authorize auth = new Authorize();

            // When a user logs in, Session[authorizationlevel] stores their auth level as 1, 2 ,3 or null.  From the Authorize class,
            // runs the Admin method, taking in Session cast as a int?
            // If the method returns true, only admins will be able to do this action, else returns redirect to another action.
            if (auth.Admin((int?)Session["authorizationlevel"]) == true)
            {
                int recipeId = recipeDal.NewRecipe(recipe);
                string[] tagArray = recipe.Tags.Split(' ');
                List<int> exists = recipeDal.TagsExist(recipe.Tags);
                for (int i = 0; i < tagArray.Length; i++)
                {
                    if (exists[i] > 0)
                    {
                        int tagId = recipeDal.GetTagIdIfExists(tagArray[i]);
                        recipeDal.InsertRecipeIdAndTagId(recipeId, tagId);
                    }
                    else
                    {
                        int tagId = recipeDal.GetTagIdAfterInsert(tagArray[i]);
                        recipeDal.InsertRecipeIdAndTagId(recipeId, tagId);
                    }
                }
                return RedirectToAction("RecipeConfirmation");
            }

            else {
                return RedirectToAction("Index");
            }
        }
        public ActionResult RecipeConfirmation()
        {
            return View();
        }


        public ActionResult Details (int id)
        {
            RecipeModel details = recipeDal.RecipeDetail(id);
            return View(details);
            
        }

    }
}