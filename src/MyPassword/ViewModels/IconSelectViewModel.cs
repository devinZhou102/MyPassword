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
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            SelectIconComplete = parameter as Action<string>;
            return base.InitializeAsync(parameter);
        }

        private void InitIconList()
        {
            foreach (var icon in IconConst.IconDatas)
            {
                IconList.Add(
                    new IconModel
                    {
                        GroupId = 0,
                        Icon = IconHelper.GetIcon(icon),
                        TappedCommand = TappedCommand,
                    });
            }
        }


        public ICommand TappedCommand => new RelayCommand<IconModel>(async (item)=>
        {
            SelectIconComplete?.Invoke(item.Icon);
            await NavigationService.PopPopupAsync();
        });

    }

    public class IconModel
    {
        public int GroupId { get; set; }
        public string Icon { get; set; }

        public ICommand TappedCommand { get; set; }
    }
}
