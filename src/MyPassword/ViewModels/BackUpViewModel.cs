using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
            //CreateGraphClientAsync();
        }

        private void InportDataExcute()
        {

        }

        public ICommand ExportDataCommand { get; private set; }
        public ICommand InportDataCommand { get; private set; }

    }
}
