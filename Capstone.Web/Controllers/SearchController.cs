using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.IO;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPlanSqlDAL planDal;
        private readonly IRecipeSqlDAL recipeDal;
        private readonly IUserSqlDAL userDal;
        private readonly ISearchSqlDAL searchDal;

        public SearchController(IPlanSqlDAL planDal, IRecipeSqlDAL recipeDal, IUserSqlDAL userDal, ISearchSqlDAL searchDal)
        {
            this.planDal = planDal;
            this.recipeDal = recipeDal;
            this.userDal = userDal;
            this.searchDal = searchDal;
        }

        
        // GET: Search
        public ActionResult Search()
        {
            SearchModel model = new SearchModel();
            List<string> cats = recipeDal.GetCategories();
            Dictionary<string, bool> catsDictionary = new Dictionary<string, bool>();
            foreach(string s in cats)
            {
                catsDictionary[s] = false;
            }
            model.SearchCategories = catsDictionary;
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            model.RecipeHash = searchDal.GetSearchResults(model);

            List<string> cats = recipeDal.GetCategories();
            Dictionary<string, bool> catsDictionary = new Dictionary<string, bool>();
            foreach (string s in cats)
            {
                catsDictionary[s] = false;
            }
            model.SearchCategories = catsDictionary;

            

            return View(model);
        }
        public ActionResult SearchResults(HashSet<RecipeModel> recipes)
        {
            return View();
        }
    }
}