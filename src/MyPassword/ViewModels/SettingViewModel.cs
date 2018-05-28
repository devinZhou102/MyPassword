using GalaSoft.MvvmLight;
using MyPassword.Helpers;
using MyPassword.Models;
using MyPassword.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyPassword.ViewModels
{
    public class SettingViewModel:ViewModelBase
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
        

        private void InitSettingItemList()
        {
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconSecret"),
                Title = "加密密钥",
                Description = "用于数据加密",
                PageType = typeof(SecureKeyPage)
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconLock"),
                Title = "手势密码",
                Description = "用于保护APP的使用权",
                PageType = typeof(GuestureLockPage)
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconBackup"),
                Title = "数据备份",
                Description = "备份您的数据",
                PageType = typeof(BackUpPage)

            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconHelper.GetIcon("IconAbout"),
                Title = "关于",
                Description = "APP相关信息",
                PageType = typeof(AboutPage)
            });
        }

    }
}
