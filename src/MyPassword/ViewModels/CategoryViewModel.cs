using GalaSoft.MvvmLight.Command;
using MyPassword.Pages;
using MyPassword.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

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

        public ICommand ItemClickCommand { get; set; }
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


        public CategoryViewModel(ICategoryService categoryService)
        {
            foreach(var c in categoryService.Categories)
            {
                CategoryItems.Add(new CategoryItemModel
                {
                    Name = c.Name,
                    Icon = c.Icon,
                    Key = c.Key,
                    Count = 0,
                    ItemClickCommand = ItemClickCommand
                });
            }
        }

        private ICommand ItemClickCommand => new RelayCommand<CategoryItemModel>((c)=> {
            alertService.DisplayAlert("密钥", ""+c.Name, "确定");
        });

        public ICommand SearchCommand => new RelayCommand(() => {
            alertService.DisplayAlert("密钥","功能开发中...","确定");
        });
        public ICommand AddCommand => new RelayCommand(() => {
            NavigationService.PushAsync(new PasswordEditPage());
        });
    }
}
