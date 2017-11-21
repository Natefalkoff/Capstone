﻿using System;
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
        private string GetPlanSql = "";

        public PlanSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int CreatePlan (string planName)
        {
            int planId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(createPlanSql, conn);
                    cmd.Parameters.AddWithValue("@plan_name", planName);
                    planId = (int)cmd.ExecuteScalar();
                }
                return planId;
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public List<PlanModel> GetPlan(int id)
        {
            List<PlanModel> result = new List<PlanModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {  
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetPlanSql, conn);
                    cmd.Parameters.AddWithValue("@plan_id", id);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        PlanModel p = new PlanModel();
                        p.PlanName = Convert.ToString(results["plan_name"]);
                        p.UserId = Int32.Parse((results["user_id"].ToString()));
                        p.Meal = Convert.ToString(results["meal_name"]);
                        p.Day = Convert.ToString(results["day_of_week"]);
                        p.RecipeId = Int32.Parse(results["recipe_id]"].ToString());
                    
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return m;
        }

    }
}