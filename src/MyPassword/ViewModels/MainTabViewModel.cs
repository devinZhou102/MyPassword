using GalaSoft.MvvmLight;
using MyPassword.Helpers;
using MyPassword.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.ViewModels
{
    public class MainTabViewModel:ViewModelBase
    {

        
        #region 属性

        private List<BaseContentPage> _TabPageList;

        public List<BaseContentPage> TabPageList
        {
            get
            {
                if (_TabPageList == null)
                {
                    _TabPageList = new List<BaseContentPage>();
                }
                return _TabPageList;
            }
            set
            {
                _TabPageList = value;
                RaisePropertyChanged(nameof(TabPageList));
            }
        }
        #endregion

        #region 初始化

        public MainTabViewModel()
        {
            InitData();
        }
        

        public void InitData()
        {
            TabPageList.Add(new CategoryListPage() { Title = "分类", Icon = IconHelper.GetIcon("IconKey") });
            TabPageList.Add(new PasswordListPage() { Title = "密钥", Icon = IconHelper.GetIcon("IconKey") });
            TabPageList.Add(new SettingPage() { Title = "设置", Icon = IconHelper.GetIcon("IconSetting") });
        }


        #endregion
    }
}
