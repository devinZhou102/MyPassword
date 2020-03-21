using GalaSoft.MvvmLight.Command;
using MyPassword.Models;
using MyPassword.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PwdGenerateViewModel:BaseViewModel
    {
        IPwdGenerateService pwdGenerateService;
        public PwdGenerateViewModel(IPwdGenerateService pwdGenerateService)
        {
            this.pwdGenerateService = pwdGenerateService;
            CheckedCharacter = true;
            CheckedLowerCase = true;
            CheckedUpperCase = true;
            CheckedNumber = true;
            Length = 10;
        }

        private bool _CheckedNumber;
        public bool CheckedNumber
        {
            get
            {
                return _CheckedNumber;
            }
            set
            {
                _CheckedNumber = value;
                RaisePropertyChanged(nameof(CheckedNumber));
            }
        }

        private bool _CheckedUpperCase;
        public bool CheckedUpperCase
        {
            get
            {
                return _CheckedUpperCase;
            }
            set
            {
                _CheckedUpperCase = value;
                RaisePropertyChanged(nameof(CheckedUpperCase));
            }
        }

        private bool _CheckedLowerCase;
        public bool CheckedLowerCase
        {
            get
            {
                return _CheckedLowerCase;
            }
            set
            {
                _CheckedLowerCase = value;
                RaisePropertyChanged(nameof(CheckedLowerCase));
            }
        }

        private bool _CheckedCharacter;
        public bool CheckedCharacter
        {
            get
            {
                return _CheckedCharacter;
            }
            set
            {
                _CheckedCharacter = value;
                RaisePropertyChanged(nameof(CheckedCharacter));
            }
        }

        private string _Password;
        public string Password 
        {
            get => _Password ?? (_Password = "");
            set
            {
                _Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private int _Length;

        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                _Length = value;
                RaisePropertyChanged(nameof(Length));
            }
        }

        Action<string> actionGenerate;

        public override Task InitializeAsync<T>(T parameter)
        {
            actionGenerate = parameter as Action<string>;
            Password = "";
            return base.InitializeAsync(parameter);
        }

        public PwdGeneratorParams BuildParams()
        {
            return new PwdGeneratorParams
            {
                HasCharactor = CheckedCharacter,
                HasLetterLowerCase = CheckedLowerCase,
                HasLetterUpperCase = CheckedUpperCase,
                HasNumber = CheckedNumber,
                Length = Length
            };

        }

        public ICommand GenerateCommand => new RelayCommand(() => 
        {
            var param = BuildParams();
            if(param.IsOk())
            {
                Password = pwdGenerateService.Generate(param);
            }

        });

        public ICommand EnsureCommand => new RelayCommand(async ()=> 
        {
            actionGenerate?.Invoke(Password);
            await NavigationService.PopAsync();
        });
    }
}
