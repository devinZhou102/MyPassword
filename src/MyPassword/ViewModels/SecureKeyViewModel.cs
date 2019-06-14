using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Manager;
using System;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class SecureKeyViewModel:ViewModelBase
    {

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
                _SecureKey = value;
                RaisePropertyChanged(nameof(SecureKey));
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

        readonly Action ActionSave;
        public SecureKeyViewModel(Action actionSave)
        {
            ActionSave = actionSave;
            HideSecureKey = true;
            SecureKey = SecureKeyManager.Instance.SecureKey;
            SaveCommand = new RelayCommand(()=>SaveExcute());
        }

        public ICommand SaveCommand { get; private set; }

        private void SaveExcute()
        {
            var oldKey = SecureKeyManager.Instance.SecureKey;
            if(!string.IsNullOrEmpty(SecureKey) && !oldKey.Equals(SecureKey))
            {
                SaveSecureKeyAsync();
            }
            else
            {
                //todo show key error
            }
        }


        private async void SaveSecureKeyAsync()
        {
            var result = await SecureKeyManager.Instance.SaveAsync(SecureKey);
            if (result)
            {
                ActionSave?.Invoke();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("设置密钥", "设置密钥失败...", "取消");
            }
        }


    }
}
