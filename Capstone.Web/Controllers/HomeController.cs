using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.IO;
using System.Web.Security;
using Capstone.Web.Crypto;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class HomeController : CapstoneController
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;
        //private UserModel user;

        public HomeController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal, UserModel user) : base(userDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
            //this.user = Session["user"] as UserModel;
        }
        //user = Session["user"] as UserModel;
        // GET: Home
        public ActionResult Index()
        {
            
            List<RecipeModel> recipes = new List<RecipeModel>();
            recipes = recipeDal.GetPublicApprovedRecipes();


            return View(recipes);
        }

        public ActionResult AddRecipe()
        {
            RecipeModel model = new RecipeModel();
            Dictionary<string, bool> choose = new Dictionary<string, bool>();
            List<string> cats = recipeDal.GetCategories();
            foreach (string s in cats)
            {
                choose[s] = false;
            }
            model.ChoseCategory = choose;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddRecipe(RecipeModel recipe, HttpPostedFileBase ImageName)
        {

            // When a user logs in, Session[authorizationlevel] stores their auth level as 1, 2 ,3 or null.  From the Authorize class,
            // runs the Admin method, taking in Session cast as a int?
            // If the method returns true, only admins will be able to do this action, else returns redirect to another action.
            if (Authorize.Admin((int?)Session["authorizationlevel"]) == true)
            {
                
                string fileName = "";
                try
                {
                    if (ImageName.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(ImageName.FileName);
                        string path = Path.Combine(Server.MapPath("~/Img/"), fileName);
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
                string[] tagArray = recipe.Tags.Split(';');
                List<int> exists = recipeDal.TagsExist(recipe.Tags);
                if (tagArray.Length != 0)
                {
                    for (int i = 0; i < tagArray.Length; i++)
                    {
                        if (exists[i] > 0)
                        {
                            int tagId = recipeDal.GetTagIdIfExists(tagArray[i].TrimStart(' '));
                            recipeDal.InsertRecipeIdAndTagId(recipeId, tagId);
                        }
                        else
                        {
                            int tagId = recipeDal.GetTagIdAfterInsert(tagArray[i].TrimStart(' '));
                            recipeDal.InsertRecipeIdAndTagId(recipeId, tagId);
                        }
                    }
                }
                foreach (KeyValuePair<string, bool> kvp in recipe.ChoseCategory)
                {
                    if (kvp.Value == true)
                    {
                        int catId = recipeDal.GetCategoryId(kvp.Key);
                        recipeDal.InsertRecipeAndCategoryId(recipeId, catId);
                    }
                }

                return RedirectToAction("RecipeConfirmation");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult RecipeConfirmation()
        {
            return View();
        }


        public ActionResult Details(string id)
        {
            RecipeModel details = recipeDal.RecipeDetail(Convert.ToInt32(id));
            return View(details);

        }

        public ActionResult Admin()
        {
            if (Authorize.Admin((int?)Session["authorizationlevel"]) == true)
            {
                List<RecipeModel> model = recipeDal.GetPublicNonApprovedRecipes();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult Admin(List<RecipeModel> model)
        {
            foreach(RecipeModel recipe in model)
            {
                if(recipe.IsPublics == true)
                {
                    bool x = recipeDal.UpdateApproval(recipe.RecipeID);
                }
            }

            return View();
        }
    }
}