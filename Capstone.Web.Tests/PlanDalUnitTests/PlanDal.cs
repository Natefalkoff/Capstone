using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Collections.Generic;

namespace Capstone.Web.Tests.PlanDalUnitTests
{
    [TestClass]
    public class PlanDal
    {
        private string connectionString = "Data Source = localhost\\sqlexpress;Initial Catalog = recipeDB; Integrated Security = True";


            [TestMethod]
        public void CreatePlanTest()
        {
            PlanSqlDAL planDal = new PlanSqlDAL(connectionString);

            int plan = planDal.CreatePlan("yummy");

            Assert.AreEqual(2, plan);
        }

        [TestMethod]
        public void addToUserPlanTest()
        {
            PlanSqlDAL planDal = new PlanSqlDAL(connectionString);

            bool success = planDal.AddUserAndPlanID(1, 2);

            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public void GetPlanTest()
        {
            PlanSqlDAL planDal = new PlanSqlDAL(connectionString);

            List<PlanModel> result = planDal.GetPlan(1);

            Assert.AreEqual(result.Count, 2);
        }
    }
}
