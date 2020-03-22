using GalaSoft.MvvmLight.Command;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{


    public class CategoryViewModel : BaseViewModel
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

        private IDatabaseService databaseService;
        private ISecureKeyService secureKeyService;

        public CategoryViewModel(ICategoryService categoryService, IDatabaseService databaseService, ISecureKeyService secureKeyService)
        {
            this.databaseService = databaseService;
            this.secureKeyService = secureKeyService;
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

        public void UpdateCategory()
        {
            Task.Run(() =>
            {
                var pwdlist = databaseService.SecureGetAll<DataItemModel>(secureKeyService.SecureKey);
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var c in CategoryItems)
                    {
                        var list = pwdlist.FindAll((p) => p.CategoryKey.Equals(c.Key));
                        c.Count = list == null ? 0 : list.Count;
                    }
                });
            });

        }

        private ICommand ItemClickCommand => new RelayCommand<CategoryItemViewModel>((c) =>
        {
            alertService.DisplayAlert("密钥", "" + c.Name, "确定");
        });

        public ICommand SearchCommand => new RelayCommand(() =>
        {
            alertService.DisplayAlert("密钥", "功能开发中...", "确定");
        });
        public ICommand AddCommand => new RelayCommand(async () =>
        {
            await NavigationService.PushAsync(new PasswordEditPage());
        });
    }
}
