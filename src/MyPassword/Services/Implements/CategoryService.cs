using MyPassword.Localization;
using MyPassword.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    }
}
