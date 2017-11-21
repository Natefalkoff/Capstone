using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public static class Authorize
    {
        public static bool Admin (int? authorizationlevel)
        {
            if (authorizationlevel == 3)
            {
                return true;
            }
            return false;
        }

        
        public static bool Registered (int? authorizationlevel)
        {
            if (authorizationlevel == 2)
            {
                return true;
            }
            return false;
        }

        public static bool Public (int? authorizationlevel)
        {
            if (authorizationlevel == 1 || authorizationlevel == null)
            {
                return true;
            }
            return false;
        }


    }
}