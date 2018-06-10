using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class IconSelectViewModel:ViewModelBase
    {
        private ObservableCollection<string> _IconList;
        public ObservableCollection<string> IconList
        {
            get
            {
                if(_IconList == null)
                {
                    _IconList = new ObservableCollection<string>();
                }
                return _IconList;
            }
            set
            {
                _IconList = value;
                RaisePropertyChanged(nameof(IconList));
            }
        }

        public IconSelectViewModel()
        {
            InitIconList();
            TappedCommand = new RelayCommand(()=> TappedExcute());
        }

        private void InitIconList()
        {
            foreach(var icon in IconConst.IconDatas)
            {
                IconList.Add(IconHelper.GetIcon(icon));
            }
        }


        public ICommand TappedCommand { get; private set; }

        private void TappedExcute()
        {

        }
    }
}
