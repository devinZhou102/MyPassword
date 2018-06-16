using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Const;
using MyPassword.Helpers;
using MyPassword.Manager;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordEditViewModel : ViewModelBase
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

        public PasswordEditViewModel(DataItemModel dataItem)
        {
            DataItem = dataItem;
            GenerateCommand = new RelayCommand(() => GenerateExcute());
            SaveCommand = new RelayCommand(() => SaveExcuteAsync());
            ImageTapCommand = new RelayCommand(() => ImageTapExcute());
            if (DataItem != null)
            {
                Icon = DataItem.Icon;
                Account = DataItem.Account;
                Title = DataItem.Name;
                Password = DataItem.Password;
                Description = DataItem.Description;
            }
            HideSecureKey = true;
        }

        
        public ICommand SaveCommand { get; private set; }

        public ICommand GenerateCommand { get; private set; }

        public ICommand ImageTapCommand { get; private set; }

        private async void SaveExcuteAsync()
        {
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
                    result = DataBaseHelper.Instance.Database.SecureUpdate<DataItemModel>(item, SecureKeyManager.Instance.SecureKey);
                }
                else
                {
                    result = DataBaseHelper.Instance.Database.SecureInsert<DataItemModel>(item, SecureKeyManager.Instance.SecureKey);
                }
                tcs.SetResult(result == 1?item:null);
            });
            return tcs.Task;
        }

        private void GenerateExcute()
        {
            var param = ConditionViewModel.BuildParams();
            if(param.IsOk())
            {
                Password = PwdGenerator.GetInstance().Generate(param);
            }
        }

        private void ImageTapExcute()
        {
            try
            {
                //NavigationService.Navigation.PushAsync(new IconSelectPage((data) =>
                //{
                //    Icon = data;
                //}));
                NavigationService.Navigation.PushModalAsync(new IconGridPage((data) =>
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
