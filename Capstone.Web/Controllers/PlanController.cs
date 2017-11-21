using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;
        private readonly ISearchSqlDAL searchDal;

        public PlanController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal, ISearchSqlDAL searchDal)
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
                return View();
            }
            //else
            //{
            //    return RedirectToAction("Login", "User");
            //}
        }

        [HttpPost]
        public ActionResult CreatePlan (PlanModel model)
        {
            planDal.CreatePlan(model.PlanName);

            return View("Index");
        }


        public ActionResult ViewPlan (int id)
        {
            List<PlanModel> model = planDal.GetPlan(id);

            return View(model);
        }
    }
}