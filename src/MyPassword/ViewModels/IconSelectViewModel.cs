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
    public class IconSelectViewModel : BaseViewModel
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

        Action<string> SelectIconComplete;

        public IconSelectViewModel()
        {
            InitIconList();
            TappedCommand = new RelayCommand<IconModel>((param) => TappedExcute(param));
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            SelectIconComplete = parameter as Action<string>;
            return base.InitializeAsync(parameter);
        }

        private void InitIconList()
        {
            //await Task.Delay(350);
            foreach (var icon in IconConst.IconDatas)
            {
                IconList.Add(
                    new IconModel
                    {
                        GroupId = 0,
                        Icon = IconHelper.GetIcon(icon)
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
