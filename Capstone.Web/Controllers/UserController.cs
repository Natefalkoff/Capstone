using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capstone.Web.Crypto;
using System.Web.Routing;
using log4net;



namespace Capstone.Web.Controllers
{
    public class UserController : CapstoneController
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;
        

        public UserController (IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal) :base(userDal)
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
            HashProvider hash = new HashProvider();
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            UserModel user = userDal.GetUser(model.UserName);

            if (user.UserName != null)
            {
                ModelState.AddModelError("username-exists", "That user name is not available");
                return View("Register", model);
            }
            else
            {
                string password = hash.HashPasswordWithMD5(model.Password, 16, 10000);
                model.Password = password;
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
            HashProvider hash = new HashProvider();
            string password = hash.HashPasswordWithMD5(model.Password, 16, 10000);
            model.Password = password;

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
                Session["authorizationlevel"] = user.AuthorizationLevel;
                Session["username"] = user.UserName;
                Session["user"] = user;
                //Session[SessionKeys.Username] = user.Email;
                // Session[SessionKeys.UserId] = user.Id;  --not sure what this does??
            }

            return RedirectToAction("Index", "Home");
        }
    }
}