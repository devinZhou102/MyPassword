using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Helpers;
using MyPassword.Models;
using MyPassword.Pages;
using SQLite.Net.Cipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordDetailViewModel:ViewModelBase
    {
        private const string PasswordMask = "******";

        private string _Password;
        public string Password
        {
            get
            {
                if (null == _Password)
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
                if (null == _Title)
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
                if (_Description == null)
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

        private string _Icon;
        public string Icon
        {
            get
            {
                if (null == _Icon)
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
                UpdatePassword();
                RaisePropertyChanged(nameof(HideSecureKey));
            }
        }

        private DataItemModel DataItem;

        public PasswordDetailViewModel(DataItemModel data)
        {
            Update(data);
            HideSecureKey = true;
            EditCommand = new RelayCommand(() => EditExcute());
            DeleteCommand = new RelayCommand(() => DeleteExcuteAsync());
            MessengerInstance.Register<DataItemModel>(this,TokenConst.TokenUpdate, (value) => 
            {
                if(value!=null)
                {
                    Update(value);
                }
            });
        }

        private void UpdatePassword()
        {
            if(HideSecureKey)
            {
                Password = PasswordMask;
            }
            else
            {
                Password = DataItem.Password;
            }
        }

        public ICommand EditCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        private void EditExcute()
        {
           NavigationService.Navigation.PushAsync(new PasswordEditPage(DataItem));
        }

        private async void DeleteExcuteAsync()
        {
            var action = await App.Current.MainPage.DisplayAlert("删除","确认要删除数据吗?","确认","取消");
            if(action)
            {
                var dialog = UserDialogs.Instance.Loading("删除数据中...");
                bool f = await DeleteItemAsync();
                dialog.Hide();
                MessengerInstance.Send(DataItem, TokenConst.TokenDelete);
                NavigationService.PopAsync();
            }
        }

        private Task<bool> DeleteItemAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(() =>
            {
                int row = DataBaseHelper.Instance.Database.SecureDelete<DataItemModel>((DataItem as IModel).Id);
                tcs.SetResult(true);
            });
            return tcs.Task;
        }

        private void Update(DataItemModel data)
        {
            DataItem = data;
            Title = DataItem?.Name;
            Icon = DataItem?.Icon;
            Account = DataItem?.Account;
            Description = DataItem?.Description;
            Password = DataItem?.Password;
        }

        public override void Cleanup()
        {
            base.Cleanup();
            MessengerInstance.Unregister<DataItemModel>(this);
        }
    }
}
