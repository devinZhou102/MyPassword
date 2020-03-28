using GalaSoft.MvvmLight.Command;
using MyPassword.Models;
using MyPassword.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class IconModel : FontIcon
    {
        public ICommand TappedCommand { get; set; }
    }


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

        Action<FontIcon> SelectIconComplete;
        IAppIconService appIconService;

        public IconSelectViewModel(IAppIconService appIconService)
        {
            this.appIconService = appIconService;
            InitIconList();
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            SelectIconComplete = parameter as Action<FontIcon>;
            return base.InitializeAsync(parameter);
        }

        private void InitIconList()
        {
            foreach (var icon in appIconService.FontIcons)
            {
                IconList.Add(new IconModel
                {
                    Icon = icon.Icon,
                    Background = icon.Background,
                    TappedCommand = TappedCommand,
                });
            }
        }


        public ICommand TappedCommand => new RelayCommand<FontIcon>(async (item) =>
        {
            SelectIconComplete?.Invoke(item);
            await NavigationService.PopPopupAsync();
        });

    }

}
