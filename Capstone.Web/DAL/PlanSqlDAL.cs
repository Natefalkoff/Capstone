using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class PlanSqlDAL : IPlanSqlDAL
    {
        private readonly string connectionString;
        private string createPlanSql = @"INSERT INTO plan(plan_name) VALUES ( @plan_name);SELECT CAST(scope_identity() AS int);";

        public PlanSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int CreatePlan (PlanModel plan)
        {
            int planId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(createPlanSql, conn);
                    cmd.Parameters.AddWithValue("@plan_name", plan.PlanName);
                    planId = (int)cmd.ExecuteScalar();
                }
                return planId;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

    }
}