using GalaSoft.MvvmLight.Command;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using SQLite.Net.Cipher.Interfaces;
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

        private IDataBaseService databaseService;
        private ISecureKeyService secureKeyService;

        public CategoryViewModel(ICategoryService categoryService, IDataBaseService databaseService, ISecureKeyService secureKeyService)
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
                    Background = c.Background,
                    ItemClickCommand = ItemClickCommand
                });
            }
        }

        public void UpdateCategory()
        {
            Task.Run(() =>
            {
                string query = $"Select * from {DataItemModel.TableName}";
                var pwdlist = databaseService.SecureQuery<DataItemModel>(query, secureKeyService.SecureKey);
                if (pwdlist != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var c in CategoryItems)
                        {
                            var list = pwdlist.FindAll((p) => p.CategoryKey?.Equals(c.Key) == true);
                            c.Count = list == null ? 0 : list.Count;
                        }
                    });
                }
            });
        }

        private ICommand ItemClickCommand => new RelayCommand<CategoryItemViewModel>(async (c) =>
        {
            await NavigationService.PushAsync(new PasswordPage(c.Key));
        });

        public ICommand SearchCommand => new RelayCommand(async () =>
        {
            await NavigationService.PushAsync(new PasswordPage());
        });
        public ICommand AddCommand => new RelayCommand(async () =>
        {
            await NavigationService.PushAsync(new PasswordEditPage());
        });
    }
}
