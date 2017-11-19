using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

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
            recipeDal.NewRecipe(recipe);

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