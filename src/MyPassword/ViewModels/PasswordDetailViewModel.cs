using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Models;
using MyPassword.Pages;
using System;
using System.Collections.Generic;
using System.Text;
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
            EditCommand = new RelayCommand(()=> EditExcute());
            MessengerInstance.Register<DataItemModel>(this, (value) => 
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

        private void EditExcute()
        {
           NavigationService.Navigation.PushAsync(new PasswordEditPage(DataItem));
        }


        private void Update(DataItemModel data)
        {
            DataItem = data;
            Title = DataItem?.Name;
            Account = DataItem?.Account;
            Description = DataItem?.Description;
        }

        public override void Cleanup()
        {
            base.Cleanup();
            MessengerInstance.Unregister<DataItemModel>(this);
        }
    }
}
