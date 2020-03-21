using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Const;
using MyPassword.Helpers;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordEditViewModel : BaseViewModel
    {

        private PwdConditionViewModel _ConditionViewModel;

        public PwdConditionViewModel ConditionViewModel
        {
            get
            {
                if (null == _ConditionViewModel)
                {
                    _ConditionViewModel = new PwdConditionViewModel();
                }
                return _ConditionViewModel;
            }
            set
            {
                _ConditionViewModel = value;
                RaisePropertyChanged(nameof(ConditionViewModel));
            }
        }

        private string _Password;

        public string Password
        {
            get
            {
                if(null == _Password)
                {
                    _Password = "";
                }
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private string _Title;
        public string Title
        {
            get
            {
                if(null == _Title)
                {
                    _Title = "";
                }
                return _Title;
            }
            set
            {
                _Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private string _Account;
        public string Account
        {
            get
            {
                if (null == _Account)
                {
                    _Account = "";
                }
                return _Account;
            }
            set
            {
                _Account = value;
                RaisePropertyChanged(nameof(Account));
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                if(_Description == null)
                {
                    _Description = "";
                }
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }
        
        private bool _HideSecureKey;
        public bool HideSecureKey
        {
            get
            {
                return _HideSecureKey;
            }
            set
            {
                _HideSecureKey = value;
                RaisePropertyChanged(nameof(HideSecureKey));
            }
        }

        private string _Icon;
        public string Icon
        {
            get
            {
                if(null == _Icon)
                {
                    _Icon = "";
                }
                return _Icon;
            }
            set
            {
                _Icon = value;
                RaisePropertyChanged(nameof(Icon));
            }
        }


        DataItemModel DataItem;
        private ISecureKeyService secureKeyService;

        public PasswordEditViewModel(ISecureKeyService secureKeyService)
        {
            this.secureKeyService = secureKeyService;
            GenerateCommand = new RelayCommand(() => GenerateExcuteAsync());
            SaveCommand = new RelayCommand(() => SaveExcuteAsync());
            ImageTapCommand = new RelayCommand(async () => await ImageTapExcuteAsync());
          
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            if (parameter != null && parameter is DataItemModel dataItem)
            {
                DataItem = dataItem;
                Icon = DataItem.Icon;
                Account = DataItem.Account;
                Title = DataItem.Name;
                Password = DataItem.Password;
                Description = DataItem.Description;
            }
            HideSecureKey = true;
            return base.InitializeAsync(parameter);
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand GenerateCommand { get; private set; }

        public ICommand ImageTapCommand { get; private set; }

        private async void SaveExcuteAsync()
        {

            if (!IsValid())
            {
                return;
            }

            var dialog = UserDialogs.Instance.Loading("数据保存中...");
            var success = await SavePassword();
            if (success != null)
            {
                UserDialogs.Instance.Toast("保存数据成功");
                Messenger.Default.Send(GetDataItemModel(success.Id),TokenConst.TokenUpdate);
                NavigationService.PopAsync();
            }
            else
            {
                UserDialogs.Instance.Toast("保存数据失败");
            }
            dialog.Hide();
        }

        private DataItemModel GetDataItemModel(int id)
        {
            var item = new DataItemModel
            {
                Id = id,
                Icon = Icon,
                Account = Account,
                Password = Password,
                Name = Title,
                Description = Description
            };
            return item;
        }

        private bool IsValid()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(Account.Trim())) isValid = false;
            else if (string.IsNullOrEmpty(Password.Trim())) isValid = false;
            else if (string.IsNullOrEmpty(Title.Trim())) isValid = false;
            else if (string.IsNullOrEmpty(Description.Trim())) isValid = false;
            if(!isValid)
            {
                App.Current.MainPage.DisplayAlert("保存","请输入有效数据...","确定");
            }
            return isValid;
        }

        private Task<DataItemModel> SavePassword()
        {
            var tcs = new TaskCompletionSource<DataItemModel>();
            Task.Factory.StartNew(() =>
            {
                var item = new DataItemModel
                {
                    Icon = Icon,
                    Account = Account,
                    Password = Password,
                    Name = Title,
                    Description = Description
                };
                int result = 0;
                if (DataItem != null)
                {
                    item.Id = DataItem.Id;
                    result = DataBaseHelper.Instance.Database.SecureUpdate<DataItemModel>(item, secureKeyService.SecureKey);
                }
                else
                {
                    result = DataBaseHelper.Instance.Database.SecureInsert<DataItemModel>(item, secureKeyService.SecureKey);
                }
                tcs.SetResult(result == 1?item:null);
            });
            return tcs.Task;
        }

        private async Task GenerateExcuteAsync()
        {
            await NavigationService.PushAsync(new PwdGeneratePage((pwd) => 
            {
                Password = pwd;
            }));
        }

        private async Task ImageTapExcuteAsync()
        {
            try
            {
                await NavigationService.PushPopupPageAsync(new IconSelectPage((data) =>
                {
                    Icon = data;
                }));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
