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

        private List<TabContentPage> _TabPageList;

        public List<TabContentPage> TabPageList
        {
            get
            {
                if (_TabPageList == null)
                {
                    _TabPageList = new List<TabContentPage>();
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
        
        

        private void InitData()
        {
            TabPageList.Add(new TabContentPage() { Title = "密码", Icon = IconHelper.GetIcon("IconKey"), TabType = TabContentPage.TAB_PWD });
            TabPageList.Add(new TabContentPage() { Title = "设置", Icon = IconHelper.GetIcon("IconSetting"), TabType = TabContentPage.TAB_SET });
        }


        #endregion
    }
}
