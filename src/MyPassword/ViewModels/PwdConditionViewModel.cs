using GalaSoft.MvvmLight;
using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.ViewModels
{
    public class PwdConditionViewModel:ViewModelBase
    {

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


        private bool _CheckedCharactor;
        public bool CheckedCharactor
        {
            get
            {
                return _CheckedCharactor;
            }
            set
            {
                _CheckedCharactor = value;
                RaisePropertyChanged(nameof(CheckedCharactor));
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


        public PwdConditionViewModel()
        {
            CheckedCharactor = true;
            CheckedLowerCase = true;
            CheckedUpperCase = true;
            CheckedNumber = true;
        }

        public PwdGeneratorParams BuildParams()
        {
            return new PwdGeneratorParams
            {
                HasCharactor = CheckedCharactor,
                HasLetterLowerCase = CheckedLowerCase,
                HasLetterUpperCase = CheckedUpperCase,
                HasNumber = CheckedNumber,
                Length = Length
            };

        }
    }
}
