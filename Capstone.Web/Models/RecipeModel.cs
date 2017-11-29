using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }

        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Directions { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        //[Required]
        public string Ingredients { get; set; }

        //[DataType(DataType.Upload)]
        public string ImageName { get; set; }

        //[DataType(DataType.Upload)]
        //HttpPostedFileBase ImageUpload { get; set; }
        public List<string> TagsList { get; set; }
        //[Required]
        public string Tags { get; set; }
        public List<string> Categories { get; set; }
        public int Publics { get; set; }
        public List<bool> Chosen { get; set; }

        //[Required]
        public Dictionary<string, bool> ChoseCategory { get; set; }
        public int Approved { get; set; }
        public bool IsPublics { get; set; }
        public string PublicOrPrivate { get; set; }
        public string TempTags { get; set; }
        public List<UserModel> SubscribedUsers { get; set; }
        public bool DeleteRecipe { get; set; }
    }
}