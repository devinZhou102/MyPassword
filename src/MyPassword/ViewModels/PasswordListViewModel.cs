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
            LoadData();
        }

        public void LoadData()
        {
            var datas = DataBaseHelper.Instance.Database?.SecureGetAll<DataItemModel>(SecureKeyManager.Instance.SecureKey);
            PasswordList.Clear();
            if (null != datas)
            {
                foreach (var item in datas)
                {
                    PasswordList.Add(item);
                }
            }

        }
        
    }
}
