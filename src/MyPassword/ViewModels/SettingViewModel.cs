using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Helpers;
using MyPassword.Localization;
using MyPassword.Models;
using MyPassword.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public class SettingViewModel:BaseViewModel
    {
        private ObservableCollection<SettingItemModel> _SettingItemList;

        public ObservableCollection<SettingItemModel> SettingItemList
        {
            get
            {
                if (_SettingItemList == null)
                {
                    _SettingItemList = new ObservableCollection<SettingItemModel>();
                }
                return _SettingItemList;
            }
            set
            {
                _SettingItemList = value;
                RaisePropertyChanged(nameof(SettingItemList));
            }
        }

        public SettingViewModel()
        {
            InitSettingItemList();
        }

        public ICommand TappedCommand => new RelayCommand<SettingItemModel>(async (item) => 
        {
            if (item.SecureProtect)
            {
                await NavigationService.PushModalAsync(new GuestureVerifyPage(async () =>
                {
                    PushPage(item);
                    await NavigationService.PopModalAsync();
                }, true));
            }
            else
            {
                PushPage(item);
            }

        });

        private void PushPage(SettingItemModel item)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var paramTpyes = new Type[0];
                var constructor = item.PageType.GetConstructor(paramTpyes);
                if (constructor != null)
                {
                    var page = constructor.Invoke(null) as Page;
                    await NavigationService.PushAsync(page);
                }
            });
        }

        private void InitSettingItemList()
        {
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Key,
                Title = AppResource.ItemSecureKey,
                Description = AppResource.ItemSecureKeyDesc,
                SecureProtect = true,
                PageType = typeof(ChangeSecureKeyPage),
                TappedCommand = TappedCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Lock,
                Title = AppResource.ItemGuesture,
                SecureProtect = true,
                Description = AppResource.ItemGuestureDesc,
                PageType = typeof(GuestureLockPage),
                TappedCommand = TappedCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Backup,
                SecureProtect = true,
                Title = AppResource.ItemBackup,
                Description = AppResource.ItemBackupDesc,
                PageType = typeof(BackUpPage),
                TappedCommand = TappedCommand

            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.About,
                Title = AppResource.ItemAbout,
                SecureProtect = false,
                Description = AppResource.ItemAboutDesc,
                PageType = typeof(AboutPage),
                TappedCommand = TappedCommand
            });
        }

    }
}
