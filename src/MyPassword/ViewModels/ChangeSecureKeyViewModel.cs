using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Models;
using MyPassword.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class ChangeSecureKeyViewModel:BaseViewModel
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
                _OldSecureKey = value.Trim();
                RaisePropertyChanged(nameof(OldSecureKey));
            }
        }

        private string _NewSecureKey;
        public string NewSecureKey
        {
            get
            {
                if(_NewSecureKey == null)
                {
                    _NewSecureKey = "";
                }
                return _NewSecureKey;
            }
            set
            {
                _NewSecureKey = value.Trim();
                RaisePropertyChanged(nameof(NewSecureKey));
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
                _ConfirmSecureKey = value.Trim();
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

        private readonly ISecureKeyService secureKeyService;
        private IDataBaseService dataBaseService;
        public ChangeSecureKeyViewModel(IDataBaseService dataBaseService, ISecureKeyService secureKeyService)
        {
            this.dataBaseService = dataBaseService;
            this.secureKeyService = secureKeyService;
            SaveCommand = new RelayCommand(()=>SaveExcuteAsync());
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            OldSecureKey = "";
            NewSecureKey = "";
            ConfirmSecureKey = "";
            CurrentKey = secureKeyService.SecureKey;
            string query = $"Select * from {DataItemModel.TableName}";
            DataList = dataBaseService.SecureQuery<DataItemModel>(query, secureKeyService.SecureKey);
            return base.InitializeAsync(parameter);
        }

        private Task<bool> UpdateDatabase(string secureKey)
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    dataBaseService.DeleteTable(typeof(DataItemModel));
                    dataBaseService.CreateTable(typeof(DataItemModel));
                    //todo  待验证
                    //await DataBaseHelper.Instance.ConnectDataBase("mypassword");
                    if (DataList != null)
                    {
                        foreach (var item in DataList)
                        {
                            dataBaseService.SecureInsert(item, secureKey);
                        }
                    }
                    var result = await SaveSecureKeyAsync();
                    tcs.SetResult(result);
                }
                catch(Exception e)
                {
                    tcs.SetResult(false);
                }
            });
            return tcs.Task;
        }

        private async Task<bool> SaveSecureKeyAsync()
        {
           return await secureKeyService.SaveAsync(NewSecureKey);
        }


        public ICommand SaveCommand { get; private set; }

        private async void SaveExcuteAsync()
        {
            if(CheckSecureKey())
            {
                var dialogProgress = UserDialogs.Instance.Loading("数据处理中...");
                bool success = await UpdateDatabase(NewSecureKey);
                dialogProgress.Hide();
                if (!success)
                {
                    UserDialogs.Instance.Toast("修改密钥失败");
                    RevertData();
                }
                else
                {
                    UserDialogs.Instance.Toast("修改密钥成功");
                    await NavigationService.PopAsync();
                }
                MessengerInstance.Send<int>(TokenConst.TokenUpdateList);
            }
        }

        private async void RevertData()
        {
            var dialogProgress = UserDialogs.Instance.Loading("正在还原数据...");
            bool success = await UpdateDatabase(CurrentKey);
            dialogProgress.Hide();
            if(success)
            {
                UserDialogs.Instance.Toast("还原数据成功");
            }
            else
            {
                UserDialogs.Instance.Toast("还原数据失败，请从备份中还原数据");
            }
        }

        private bool CheckSecureKey()
        {
            if (!CurrentKey.Equals(OldSecureKey))
            {
                ErrorMsg = "原密钥不正确,请重新输入";
                return false;
            }
            else if(NewSecureKey.Length < 8)
            {
                ErrorMsg = "请输入不少于8位的密钥";
                return false;
            }
            else if(!NewSecureKey.Equals(ConfirmSecureKey))
            {
                ErrorMsg = "两次密钥输入不一样";
                return false;
            }
            else if(NewSecureKey.Equals(CurrentKey))
            {
                ErrorMsg = "新密钥不能与旧密钥一致";
                return false;
            }
            return true;
        }
    }
}
