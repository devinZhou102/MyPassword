using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class CategoryItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Key { get; set; }

        private int _Count;
        public int Count
        {
            get => _Count;
            set
            {
                _Count = value;
                RaisePropertyChanged(nameof(Count));
            }
        }

        public bool IsSelected { get; set; }

        public ICommand ItemClickCommand { get; set; }
    }

}
