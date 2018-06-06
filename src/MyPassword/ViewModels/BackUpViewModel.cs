using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class BackUpViewModel:ViewModelBase
    {

        public BackUpViewModel()
        {
            ExportDataCommand = new RelayCommand(() => ExportDataExcute());
            InportDataCommand = new RelayCommand(() => InportDataExcute());
        }


        private void ExportDataExcute()
        {

        }

        private void InportDataExcute()
        {

        }

        public ICommand ExportDataCommand { get; private set; }
        public ICommand InportDataCommand { get; private set; }
    }
}
