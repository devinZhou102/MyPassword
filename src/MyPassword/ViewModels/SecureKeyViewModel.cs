using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class SecureKeyViewModel:BaseViewModel
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

        Action ActionSave;
        private ISecureKeyService secureKeyService;
        public SecureKeyViewModel(ISecureKeyService secureKeyService)
        {
            HideSecureKey = true;
            this.secureKeyService = secureKeyService;
            SecureKey = secureKeyService.SecureKey;
            SaveCommand = new RelayCommand(()=>SaveExcute());
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            ActionSave = parameter as Action;
            return base.InitializeAsync(parameter);
        }

        public ICommand SaveCommand { get; private set; }

        private void SaveExcute()
        {
            var oldKey = secureKeyService.SecureKey;
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
            var result = await secureKeyService.SaveAsync(SecureKey);
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
