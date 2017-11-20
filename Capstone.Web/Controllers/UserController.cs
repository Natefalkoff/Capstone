using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;

        public UserController (IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
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

            UserModel user = userDal.GetUser(model.UserName);

            if (user != null)
            {
                ModelState.AddModelError("username-exists", "That user name is not available");
                return View("Register", model);
            }
            else
            {
                model.AuthorizationLevel = 2;
                userDal.RegisterUser(model);

                FormsAuthentication.SetAuthCookie(user.Email, true);
                // Session[SessionKeys.Username] = model.EmailAddress;
                //Session[SessionKeys.UserId] = user.Id;  ??? whats session keys
            }

            return RedirectToAction("RegisterSuccess");
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            UserModel user = userDal.GetUser(model.UserName);



            // user does not exist or password is wrong
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                //Session[SessionKeys.Username] = user.Email;
                // Session[SessionKeys.UserId] = user.Id;  --not sure what this does??
            }

            return RedirectToAction("Index", "Home");
        }
    }
}