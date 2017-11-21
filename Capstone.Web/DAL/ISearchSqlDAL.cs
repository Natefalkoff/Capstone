using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public interface ISearchSqlDAL
    {
        HashSet<RecipeModel> GetSearchResults(SearchModel model);
    }
}
