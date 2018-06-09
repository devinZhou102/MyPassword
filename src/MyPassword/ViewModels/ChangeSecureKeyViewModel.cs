using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Helpers;
using MyPassword.Manager;
using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class ChangeSecureKeyViewModel:ViewModelBase
    {

        private string _OldSecureKey;
        public string OldSecureKey
        {
            get
            {
                if(_OldSecureKey == null)
                {
                    _OldSecureKey = "";
                }
                return _OldSecureKey;
            }
            set
            {
                _OldSecureKey = value;
                RaisePropertyChanged(nameof(OldSecureKey));
            }
        }

        private string _SecureKey;
        public string SecureKey
        {
            get
            {
                if(_SecureKey == null)
                {
                    _SecureKey = "";
                }
                return _SecureKey;
            }
            set
            {
                _SecureKey = value.Trim();
                RaisePropertyChanged(nameof(SecureKey));
            }
        }

        private string _ConfirmSecureKey;
        public string ConfirmSecureKey
        {
            get
            {
                if(_ConfirmSecureKey == null)
                {
                    _ConfirmSecureKey = "";
                }
                return _ConfirmSecureKey;
            }
            set
            {
                _ConfirmSecureKey = value;
                RaisePropertyChanged(nameof(ConfirmSecureKey));
            }
        }

        private string CurrentKey;

        private string _ErrorMsg;

        public string ErrorMsg
        {
            get
            {
                if(_ErrorMsg == null)
                {
                    _ErrorMsg = "";
                }
                return _ErrorMsg;
            }
            set
            {
                _ErrorMsg = value;
                RaisePropertyChanged(nameof(ErrorMsg));
            }
        }

        private List<DataItemModel> DataList;

        public ChangeSecureKeyViewModel()
        {
            SaveCommand = new RelayCommand(()=>SaveExcuteAsync());
            CurrentKey = SecureKeyManager.Instance.SecureKey;
            DataList = DataBaseHelper.Instance.Database.SecureGetAll<DataItemModel>(CurrentKey);
        }

        private Task<bool> ChangeSecureKey()
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    DataBaseHelper.Instance.Database.DeleteTables();
                    DataBaseHelper.Instance.ConnectDataBase("mypassword");
                    if (DataList != null)
                    {
                        foreach (var item in DataList)
                        {
                            DataBaseHelper.Instance.Database.SecureInsert(item, SecureKey);
                        }
                    }
                    bool success = SaveSecureKey();
                    tcs.SetResult(success);
                }
                catch(Exception e)
                {
                    tcs.SetResult(false);
                }
            });
            return tcs.Task;
        }

        private bool SaveSecureKey()
        {
            return SecureKeyManager.Instance.Save(SecureKey);
        }


        public ICommand SaveCommand { get; private set; }

        private async void SaveExcuteAsync()
        {
            if(CheckSecureKey())
            {
                var dialogProgress = UserDialogs.Instance.Loading("数据处理中...");
                bool success = await ChangeSecureKey();
                dialogProgress.Hide();
                if (!success)
                {
                    RevertData();
                }
            }
        }

        private void RevertData()
        {
            
        }

        private bool CheckSecureKey()
        {
            if (!CurrentKey.Equals(OldSecureKey))
            {
                ErrorMsg = "原密钥不正确,请重新输入";
                return false;
            }
            else if(SecureKey.Length < 8)
            {
                ErrorMsg = "请输入不少于8位的密钥";
                return false;
            }
            else if(!SecureKey.Equals(ConfirmSecureKey))
            {
                ErrorMsg = "两次密钥输入不一样";
                return false;
            }
            return true;
        }
    }
}
