using MyPassword.Localization;
using MyPassword.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyPassword.ViewModels
{

    public class CategoryItemModel:BaseViewModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Key { get; set; }

        private int _Count;
        public int Count 
        {
            get => _Count; 
            set
            {
                _Count = value;
                RaisePropertyChanged(nameof(Count));
            }
        }
    }

    public class CategoryViewModel:BaseViewModel
    {

        private ObservableCollection<CategoryItemModel> _CategoryItems;

        public ObservableCollection<CategoryItemModel> CategoryItems
        {
            get => _CategoryItems ?? (_CategoryItems = new ObservableCollection<CategoryItemModel>());
            set
            {
                _CategoryItems = value;
                RaisePropertyChanged(nameof(CategoryItems));
            }
        }


        public CategoryViewModel()
        {
            var category = JsonConvert.DeserializeObject<List<CategoryModel>>(AppResource.Category);
            foreach(var c in category)
            {
                CategoryItems.Add(new CategoryItemModel
                {
                    Name = c.Name,
                    Icon = c.Icon,
                    Key = c.Key,
                    Count = 0
                });
            }
        }

    }
}
