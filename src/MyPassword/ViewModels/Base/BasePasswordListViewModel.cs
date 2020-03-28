using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyPassword.ViewModels
{
    public class BasePasswordListViewModel:BaseViewModel
    {

        private ObservableCollection<DataItemModel> _DataList;

        public ObservableCollection<DataItemModel> DataList
        {
            get => _DataList ?? (_DataList = new ObservableCollection<DataItemModel>());
            set
            {
                _DataList = value;
                RaisePropertyChanged(nameof(DataList));
            }
        }


    }
}
