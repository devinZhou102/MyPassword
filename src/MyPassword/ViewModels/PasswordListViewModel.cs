using GalaSoft.MvvmLight;
using MyPassword.Helpers;
using MyPassword.Manager;
using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.ViewModels
{
    public class PasswordListViewModel:ViewModelBase
    {

        private ObservableCollection<DataItemModel> _PasswordList;

        public ObservableCollection<DataItemModel> PasswordList
        {
            get
            {
                if (null == _PasswordList)
                {
                    _PasswordList = new ObservableCollection<DataItemModel>();
                }
                return _PasswordList;
            }
            set
            {
                _PasswordList = value;
                RaisePropertyChanged(nameof(PasswordList));
            }
        }

        public PasswordListViewModel()
        {
            //TestData();
            LoadData();
        }

        private void LoadData()
        {
                var datas = DataBaseHelper.Instance.Database.SecureGetAll<DataItemModel>(SecureKeyManager.Instance.SecureKey);
                if (null != datas)
                {
                    foreach (var item in datas)
                    {
                        PasswordList.Add(item);
                    }
                }

        }

        private void TestData()
        {
            PasswordList.Add(new DataItemModel
            {
                Icon = IconHelper.GetIcon("IconSecret"),
                Name = "淘宝",
                Account = "717391514@qq.com",
                Description = "这是我的账号",
                Password ="12345678"
            });
            PasswordList.Add(new DataItemModel
            {
                Icon = IconHelper.GetIcon("IconSecret"),
                Name = "淘宝",
                Account = "717391514@qq.com",
                Description = "这是我的账号",
                Password = "12345678"
            });
            PasswordList.Add(new DataItemModel
            {
                Icon = IconHelper.GetIcon("IconSecret"),
                Name = "京东",
                Account = "717391514@qq.com",
                Description = "这是我的账号",
                Password = "12345678"
            });
        }

    }
}
