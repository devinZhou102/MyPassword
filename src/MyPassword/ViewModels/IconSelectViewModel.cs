using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class IconSelectViewModel : ViewModelBase
    {
        private ObservableCollection<IconModel> _IconList;
        public ObservableCollection<IconModel> IconList
        {
            get
            {
                if (_IconList == null)
                {
                    _IconList = new ObservableCollection<IconModel>();
                }
                return _IconList;
            }
            set
            {
                _IconList = value;
                RaisePropertyChanged(nameof(IconList));
            }
        }

        readonly Action<string> SelectIconComplete;

        public IconSelectViewModel(Action<string> selectIconComplete)
        {
            SelectIconComplete = selectIconComplete;
            InitIconListAsync();
            TappedCommand = new RelayCommand<IconModel>((param) => TappedExcute(param));
        }

        private async void InitIconListAsync()
        {
            await Task.Delay(350);
            foreach (var icon in IconConst.IconDatas)
            {
                IconList.Add(
                    new IconModel{
                        GroupId = 0,
                        Icon =IconHelper.GetIcon(icon)
                    });
            }
        }


        public ICommand TappedCommand { get; private set; }

        private void TappedExcute(IconModel item)
        {
            NavigationService.Navigation.PopModalAsync();
            //NavigationService.PopAsync();
            SelectIconComplete?.Invoke(item.Icon);
        }
    }

    public class IconModel
    {
        public int GroupId { get; set; }
        public string Icon { get; set; }
    }
}
