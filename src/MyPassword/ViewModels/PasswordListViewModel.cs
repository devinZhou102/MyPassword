using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Helpers;
using MyPassword.Manager;
using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MyPassword.Const;

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
            RegisterMessager();
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

        private void RegisterMessager()
        {
            MessengerInstance.Register<DataItemModel>(this,(data) => 
            {
                if(data != null)
                {
                  var item = PasswordList.Where((v) => v.Id == data.Id);
                  if(item != null && item.Count() > 0)
                  {
                       int index = PasswordList.IndexOf(item.First());
                       PasswordList.RemoveAt(index);
                       PasswordList.Insert(index,data);
                  }
                  else
                  {
                      PasswordList.Add(data);
                  }
                }
            });

            MessengerInstance.Register<int>(this,(value)=> 
            {
                if(value == TokenConst.TokenUpdateList)
                {
                    LoadData();
                }
            });
        }
        

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        
    }
}
