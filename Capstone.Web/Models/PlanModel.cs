﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class PlanModel
    {
        public int PlanID { get; set; }
        public string DayOfWeek { get; set; }
        public List<RecipeModel> Recipes { get; set; }

    }
}