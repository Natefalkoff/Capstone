using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class Authorize
    {
        public bool Admin (int authorizationlevel)
        {
            if (authorizationlevel == 3)
            {
                return true;
            }
            return false;
        }

        
        public bool Registered (int authorizationlevel)
        {
            if (authorizationlevel == 2)
            {
                return true;
            }
            return false;
        }

        public bool Public (int authorizationlevel)
        {
            if (authorizationlevel == 1)
            {
                return true;
            }
        }


    }
}