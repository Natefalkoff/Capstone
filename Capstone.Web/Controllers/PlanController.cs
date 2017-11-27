using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Capstone.Web.Controllers
{
    public class PlanController : CapstoneController
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;
        private readonly ISearchSqlDAL searchDal;

        public PlanController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal, ISearchSqlDAL searchDal) :base(userDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
            this.searchDal = searchDal;
        }

        // GET: Plan
        public ActionResult Index()
        {

            //if (Authorize.Registered((int?)Session["authorizationlevel"]) == true || Authorize.Admin((int?)Session["authorizationlevel"]) == true)
            {
                UserModel user = Session["user"] as UserModel;
                List<PlanModel> plans = planDal.GetAllUserPlans(user.UserID);
                foreach (PlanModel plan in plans)
                {
                    string planname = planDal.GetPlanName(plan.PlanId);
                    plan.PlanName = planname;
                }
                DropDownPlans model = new DropDownPlans();
                model.PlanModels = plans;

                
                return View(model);
            }
        }

       
        public ActionResult CreatePlan ()
        {
            PlanModel model = new PlanModel();
           
            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePlan (PlanModel model)
        {
            UserModel user = Session["user"] as UserModel;
            int planID = planDal.CreatePlan(model.PlanName);
               planDal.AddUserAndPlanID(planID, user.UserID);

            return RedirectToAction("Index", "Plan");
        }
        
        



        [HttpPost]
        public ActionResult ViewPlanID (DropDownPlans model)
        {
            return RedirectToAction("ViewPlan", "Plan", new { id = model.currentID });
        }


      
        public ActionResult ViewPlan (int id)
        {
            List<PlanModel> model = planDal.GetPlan(id);

            return View("ViewPlan", model);
        }


        public ActionResult AddMealToPlan ()
        {
            UserModel user = Session["user"] as UserModel;
            PlanModel p = new PlanModel();
            DropDownPlans model = new DropDownPlans();

            List<RecipeModel> recipes = recipeDal.GetRecipes();
            List<PlanModel> plans = planDal.GetAllUserPlans(user.UserID);
            foreach (PlanModel plan in plans)
            {
                string planname = planDal.GetPlanName(plan.PlanId);
                plan.PlanName = planname;
            }
            List<string> days = p.Days;
            List<string> meals = p.Meals;

            model.Recipes = recipes;
            model.PlanModels = plans;
            model.Days = days;
            model.Meals = meals;

            
       

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMealToPlan (DropDownPlans model)
        {
            int mealID = planDal.CreateMeal(model.selectedDay, model.selectedMeal);
            planDal.AddMealAndPlanID(mealID, model.selectedPlan);
            planDal.InsertMealAndRecipeID(mealID, model.selectedRecipe);

            return RedirectToAction("AddMealConfirmation");
        }

        public ActionResult AddMealConfirmation (DropDownPlans model)
        {
            return View();
        }
    }
}