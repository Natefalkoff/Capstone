using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.IO;

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
            RecipeModel model = new RecipeModel();
            Dictionary<string, bool> choose = new Dictionary<string, bool>();
            List<string> cats = recipeDal.GetCategories();
            foreach(string s in cats)
            {
                choose[s] = false;
            }
            model.ChoseCategory = choose;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddRecipe(RecipeModel recipe, HttpPostedFileBase ImageName)
        {
            string fileName = "";
            try
            {
                if (ImageName.ContentLength > 0)
                {
                    fileName = Path.GetFileName(ImageName.FileName);
                    string path = Path.Combine(Server.MapPath("~/App_Data/Img/"), fileName);
                    ImageName.SaveAs(path);
                }
                ViewBag.Message = "File Uploaded Successfully!";
            }
            catch
            {
                ViewBag.Message = "File Upload Failed!";
            }
            recipe.ImageName = fileName;
            int recipeId = recipeDal.NewRecipe(recipe);
            string[] tagArray = recipe.Tags.Split(' ');
            List<int> exists = recipeDal.TagsExist(recipe.Tags);
            for(int i = 0; i < tagArray.Length; i++)
            {
                if(exists[i] > 0)
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
            foreach(KeyValuePair<string, bool> kvp in recipe.ChoseCategory)
            {
                if(kvp.Value == true)
                {
                    int catId = recipeDal.GetCategoryId(kvp.Key);
                    recipeDal.InsertRecipeAndCategoryId(recipeId, catId);
                }
            }         
            return RedirectToAction("RecipeConfirmation");
        }
        public ActionResult RecipeConfirmation()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(RecipeModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            return RedirectToAction("RegisterSuccess");
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }      
    }
}