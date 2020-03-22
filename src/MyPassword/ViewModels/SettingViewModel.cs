using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Helpers;
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
                Icon = IconHelper.GetIcon("IconSecret"),
                Title = "加密密钥",
                Description = "用于数据加密",
                SecureProtect = true,
                PageType = typeof(ChangeSecureKeyPage),
                TappedCommand = TappedCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconLock"),
                Title = "手势密码",
                SecureProtect = true,
                Description = "用于保护APP的使用权",
                PageType = typeof(GuestureLockPage),
                TappedCommand = TappedCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconBackup"),
                SecureProtect = true,
                Title = "数据备份",
                Description = "备份您的数据",
                PageType = typeof(BackUpPage),
                TappedCommand = TappedCommand

            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconAbout"),
                Title = "关于",
                SecureProtect = false,
                Description = "APP相关信息",
                PageType = typeof(AboutPage),
                TappedCommand = TappedCommand
            });
        }

    }
}
