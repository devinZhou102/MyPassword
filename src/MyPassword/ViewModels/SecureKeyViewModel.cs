using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Manager;
using System;
using System.Collections.Generic;
using System.Text;
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

        public SecureKeyViewModel()
        {
            SecureKey = SecureKeyManager.Instance.SecureKey;
            SaveCommand = new RelayCommand(()=>SaveExcute());
        }

        public ICommand SaveCommand { get; private set; }

        private void SaveExcute()
        {
            var oldKey = SecureKeyManager.Instance.SecureKey;
            if(!string.IsNullOrEmpty(SecureKey) && !oldKey.Equals(SecureKey))
            {
                SecureKeyManager.Instance.Save(SecureKey);
            }
        }


    }
}
