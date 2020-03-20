using GalaSoft.MvvmLight;
using Xamarin.Essentials;

namespace MyPassword.ViewModels
{
    public class AboutViewModel:BaseViewModel
    {
        private string _AppVersion;

        public string AppVersion
        {
            get
            {
                if(_AppVersion == null)
                {
                    _AppVersion = "";
                }
                return _AppVersion;
            }
            set
            {
                _AppVersion = value;
                RaisePropertyChanged(nameof(AppVersion));
            }
        }

        private string _AppName;

        public string AppName
        {
            get => _AppName ?? (_AppName = "");
            set
            {
                _AppName = value;
                RaisePropertyChanged(nameof(AppName));
            }
        }


        public AboutViewModel()
        {
            var currentVersion = VersionTracking.CurrentVersion;
            AppVersion = currentVersion;
            AppName = Xamarin.Essentials.AppInfo.Name;
            //var vt = CrossVersionTracking.Current;
            //AppVersion = vt.CurrentVersion;
        }

    }
}
