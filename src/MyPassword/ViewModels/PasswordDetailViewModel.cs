using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordDetailViewModel:ViewModelBase
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



        public PasswordDetailViewModel()
        {
            GenerateCommand = new RelayCommand(()=>GenerateExcute());
        }



        public ICommand GenerateCommand { get; private set; }

        private void GenerateExcute()
        {
            var param = ConditionViewModel.BuildParams();
            if(param.IsOk())
            {
                Password = PwdGenerator.GetInstance().Generate(param);
            }
        }
    }
}
