using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Localization;
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
                loadingService.ShowLoading();
                bool success = await UpdateDatabase(NewSecureKey);
                loadingService.HideLoading();
                if (!success)
                {
                    alertService.Toast(AppResource.MsgChangeSecretFailed);
                    RevertData();
                }
                else
                {
                    alertService.Toast(AppResource.MsgChangeSecretSuccess);
                    await NavigationService.PopAsync();
                }
                MessengerInstance.Send<int>(TokenConst.TokenUpdateList);
            }
        }

        private async void RevertData()
        {
            loadingService.ShowLoading(AppResource.MsgRestoreData);
            bool success = await UpdateDatabase(CurrentKey);
            loadingService.HideLoading();
            if(success)
            {
                alertService.Toast(AppResource.MsgRestoreSuccess);
            }
            else
            {
                alertService.DisplayAlert("",AppResource.MsgRestoreFailed,AppResource.DialogButtonConfirm);
            }
        }

        private bool CheckSecureKey()
        {
            if (!CurrentKey.Equals(OldSecureKey))
            {
                ErrorMsg = AppResource.TipsWrongSecret;
                return false;
            }
            else if(NewSecureKey.Length < 8)
            {
                ErrorMsg = AppResource.TipsSecretLength;
                return false;
            }
            else if(!NewSecureKey.Equals(ConfirmSecureKey))
            {
                ErrorMsg = AppResource.TipsSecretConfirmFailed;
                return false;
            }
            else if(NewSecureKey.Equals(CurrentKey))
            {
                ErrorMsg = AppResource.TipsSecretSameAsOld;
                return false;
            }
            return true;
        }
    }
}
