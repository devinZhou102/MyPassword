using MyPassword.Localization;
using MyPassword.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPassword.Services
{
    public class CategoryService:ICategoryService
    {
        public List<CategoryModel> Categories { get; private set; }

        public CategoryService()
        {
            Categories = JsonConvert.DeserializeObject<List<CategoryModel>>(AppResource.Category);
        }

        public CategoryModel FindCategoryByKey(string key)
        {
            return Categories.Find((d)=>d.Key.Equals(key));
        }

        public CategoryModel GetDefaultCategory()
        {
            return Categories.ElementAt(Categories.Count - 1);
        }
    }
}
