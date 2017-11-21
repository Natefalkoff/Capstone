using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public interface IPlanSqlDAL
    {
        int CreatePlan(string planName);

        List<PlanModel> GetPlan(int id);
    }


  
}
