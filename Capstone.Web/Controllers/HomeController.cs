using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["recipeDB"].ConnectionString;

      

        // GET: Home
        public ActionResult Index()
        {
          
            return View("Index");
        }

        
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            UserSqlDAL userDal = new UserSqlDAL(connectionString);
            userDal.RegisterUser(model);

            return RedirectToAction("RegisterSuccess", "Home");
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }


    }
}