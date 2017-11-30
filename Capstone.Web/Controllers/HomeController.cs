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

        public HomeController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal) : base(userDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
            //this.user = Session["user"] as UserModel;
        }
        
        // GET: Home
        public ActionResult Index()
        {
            UserModel user = Session["user"] as UserModel;
            List<RecipeModel> recipes = new List<RecipeModel>();
            recipes = recipeDal.GetPublicApprovedRecipes();


            return View(recipes);
        }

        public ActionResult AddRecipe()
        {
            if (Authorize.Registered((int?)Session["authorizationlevel"]) == true || Authorize.Admin((int?)Session["authorizationlevel"]) == true)
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
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddRecipe(RecipeModel recipe)
        {
            if (recipe.ChoseCategory == null || recipe.Ingredients == null || recipe.Name == null || recipe.Tags == null || recipe.Directions == null)
            {
                return RedirectToAction("AddRecipe");
            }
            UserModel user = Session["user"] as UserModel;
            // When a user logs in, Session[authorizationlevel] stores their auth level as 1, 2 ,3 or null.  From the Authorize class,
            // runs the Admin method, taking in Session cast as a int?
            // If the method returns true, only admins will be able to do this action, else returns redirect to another action.
            if (Authorize.Registered((int?)Session["authorizationlevel"]) == true || Authorize.Admin((int?)Session["authorizationlevel"]) == true)
            {
                List<string> tagArray = new List<string>();
                string fileName = "";
                //try
                //{
                //    if (ImageName.ContentLength > 0)
                //    {
                //        fileName = Path.GetFileName(ImageName.FileName);
                //        string path = Path.Combine(Server.MapPath("~/Img/"), fileName);
                //        ImageName.SaveAs(path);
                //    }
                //    ViewBag.Message = "File Uploaded Successfully!";
                //}
                //catch
                //{
                //    ViewBag.Message = "File Upload Failed!";
                //}
                //recipe.ImageName = fileName;
                if (recipe.PublicOrPrivate != null)
                {
                    recipe.Publics = Int32.Parse(recipe.PublicOrPrivate);
                }
                int recipeId = recipeDal.NewRecipe(recipe);
                recipeDal.InsertRecipeIdAndUserId(user.UserID, recipeId);
                if (recipe.Tags != null)
                {
                    tagArray = recipe.Tags.Split(';').ToList<string>();
                    for(int i = 0; i < tagArray.Count; i++)
                    {
                        tagArray[i] = tagArray[i].ToLower();
                    }
                }

                List<int> exists = recipeDal.TagsExist(recipe.Tags);
                if (tagArray.Count != 0)
                {
                    for (int i = 0; i < tagArray.Count; i++)
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
            details.TagsList = recipeDal.GetTagsFromId(Convert.ToInt32(id));
            details.Categories = recipeDal.GetCatsFromId(Convert.ToInt32(id));
            return View(details);

        }

        [HttpPost]
        public ActionResult Details(RecipeModel model)
        {
            UserModel user = Session["user"] as UserModel;
            if (user != null)
            {
                model.UserID = user.UserID;
                string[] tagArray = model.Tags.Split(';');
                List<int> exists = recipeDal.TagsExist(model.Tags);
                if (tagArray.Length != 0)
                {
                    for (int i = 0; i < tagArray.Length; i++)
                    {
                        if (exists[i] > 0)
                        {
                            int tagId = recipeDal.GetTagIdIfExists(tagArray[i].TrimStart(' '));
                            recipeDal.InsertRecipeIdAndTagId(model.RecipeID, tagId);
                        }
                        else
                        {
                            int tagId = recipeDal.GetTagIdAfterInsert(tagArray[i].TrimStart(' '));
                            recipeDal.InsertRecipeIdAndTagId(model.RecipeID, tagId);
                        }
                    }
                }
            }
            return RedirectToAction("Index");

        }

        public ActionResult Admin()
        {
            if (Authorize.Admin((int?)Session["authorizationlevel"]) == true)
            {
                //ViewBag.Model = new RecipeModel();
                List<RecipeModel> model = recipeDal.GetRecipes();
                RecipeModel users = new RecipeModel();
                Session["subscribers"] = recipeDal.SubscribedUsers();
                //model.Add(users);
                ViewBag.Subsribers = recipeDal.SubscribedUsers();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Upload(int recipeId, HttpPostedFileBase file)
        {
            
            string fileName = "";
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Img/"), fileName);
                file.SaveAs(path);
                recipeDal.AddImage(fileName, recipeId);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Admin");



        }
        [HttpPost]
        public ActionResult Admin(List<RecipeModel> model)
        {

            Session["subscribers"] = recipeDal.SubscribedUsers();
            foreach (RecipeModel recipe in model)
            {
                if(recipe.IsPublics == true)
                {
                    recipeDal.UpdateApproval(recipe.RecipeID);
                }
                if(recipe.DeleteRecipe == true)
                {
                    recipeDal.DeleteRecipe(recipe.RecipeID);
                }
                
            }
            


            return RedirectToAction("Admin");
        }
    }
}