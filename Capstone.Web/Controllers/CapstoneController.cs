using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.IO;
using System.Web.Security;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class CapstoneController : Controller
    {
        private const string UsernameKey = "UserName";
        private readonly IUserSqlDAL userDal;

        public CapstoneController(IUserSqlDAL userDal)
        {
            this.userDal = userDal;
        }


        /// <summary>
        /// Gets the value from the Session
        /// </summary>
        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user cookie exists, if not create it
                if (Session[UsernameKey] != null)
                {
                    username = (string)Session[UsernameKey];
                }

                return username;
            }
        }

        /// <summary>
        /// Returns bool if user has authenticated in
        /// </summary>        
        public bool IsAuthenticated
        {
            get
            {
                return Session[UsernameKey] != null;
            }
        }

        /// <summary>
        /// "Logs" the current user in
        /// </summary>
        public void LogUserIn(string username)
        {
            //Session.Abandon();
            //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session[UsernameKey] = username;
        }

        /// <summary>
        /// "Logs out" a user by removing the cookie.
        /// </summary>
        public void LogUserOut()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }


        [ChildActionOnly]
        public ActionResult GetAuthenticatedUser()
        {
            UserModel model = null;

            if (IsAuthenticated)
            {
                model = userDal.GetUser(CurrentUser);
            }

            return PartialView("_AuthenticationBar", model);
        }
    }
}