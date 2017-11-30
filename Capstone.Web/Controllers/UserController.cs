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


        public UserController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal) : base(userDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
        }

        public ActionResult LogOut()
        {
            Session["authorizationlevel"] = null;
            Session["username"] = null;
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }


        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("users/new")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("users/new")]
        public ActionResult Register(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }


            UserModel user = userDal.GetUser(model.UserName);

            if (user.UserName != null)
            {
                ModelState.AddModelError("username-exists", "That user name is not available");
                return View("Register", model);
            }
            else
            {
                HashProvider hash = new HashProvider();
                string password = hash.HashPassword(model.Password);
                model.Salt = hash.SaltValue;
                model.Password = password;
                model.AuthorizationLevel = 2;
                if(model.SignupBool == true)
                {
                    model.Signup = 1;
                }
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
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword");
            }
            UserModel user = Session["user"] as UserModel;
            HashProvider hash = new HashProvider();
            string s = model.TempPassword;
            s = hash.HashPassword(s);


            //if (!ModelState.IsValid)
            //{
            //    return View("ChangePassword");
            //}



            if (hash.VerifyPasswordMatch(user.Password, model.TempPassword, user.Salt) == false)
            {
                return View("ChangePassword");
            }

            else
            {
                
                string password = hash.HashPassword(model.Password);
                model.Salt = hash.SaltValue;
                model.Password = password;
                model.AuthorizationLevel = 2;
                userDal.ChangePassword(model.Password, model.Salt, user.UserName);

                //FormsAuthentication.SetAuthCookie(user.Email, true);
                // Session[SessionKeys.Username] = model.EmailAddress;
                //Session[SessionKeys.UserId] = user.Id;  ??? whats session keys
            }

            return RedirectToAction("ChangeSuccess");
        }

        public ActionResult ChangeSuccess()
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
                return RedirectToAction("Login");
            }
            //HashProvider hash = new HashProvider();
            //string password = hash.HashPassword(model.Password);
            //model.Password = model.Password;

            if (ModelState.IsValid)
            {
                UserModel userLogin = userDal.GetUser(model.UserName);

                if (userLogin != null)
                {
                    HashProvider hashProvider = new HashProvider();
                    bool doesPasswordMatch = hashProvider.VerifyPasswordMatch(userLogin.Password, model.Password, userLogin.Salt);
                    if (!doesPasswordMatch)
                    {
                        ModelState.AddModelError("invalid-login", "The username or password combination is not valid");
                        return View("Login", model);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(userLogin.UserName, true);
                        Session["authorizationlevel"] = userLogin.AuthorizationLevel;
                        Session["username"] = userLogin.UserName;
                        Session["user"] = userLogin;

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("invalid-login", "The username or password combination is not valid");
                    return View("Login", model);
                }
                //return View("Login", model);
            }

            UserModel user = userDal.GetUser(model.UserName);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserPage()
        {
            UserModel m = Session["user"] as UserModel;
            int userId = Convert.ToInt32(m.UserID);
            List<RecipeModel> model = recipeDal.GetUserRecipes(userId);
            return View(model);
        }


    }
}