using GalaSoft.MvvmLight.Command;
using MyPassword.Models;
using MyPassword.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class CategorySelectViewModel:BaseViewModel
    {


        private ObservableCollection<CategoryItemViewModel> _CategoryItems;

        public ObservableCollection<CategoryItemViewModel> CategoryItems
        {
            get => _CategoryItems ?? (_CategoryItems = new ObservableCollection<CategoryItemViewModel>());
            set
            {
                _CategoryItems = value;
                RaisePropertyChanged(nameof(CategoryItems));
            }
        }

        private Action<CategoryModel> actionSelected;
        readonly ICategoryService categoryService;
        public CategorySelectViewModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            foreach (var c in categoryService.Categories)
            {
                CategoryItems.Add(new CategoryItemViewModel
                {
                    Name = c.Name,
                    Icon = c.Icon,
                    Key = c.Key,
                    Count = 0,
                    ItemClickCommand = ItemClickCommand
                });
            }
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            actionSelected = parameter as Action<CategoryModel>;
            return base.InitializeAsync(parameter);
        }


        private ICommand ItemClickCommand => new RelayCommand<CategoryItemViewModel>(async (c) => {
            actionSelected?.Invoke(categoryService.FindCategoryByKey(c.Key));
            await NavigationService.PopAsync();
        });
    }
}
