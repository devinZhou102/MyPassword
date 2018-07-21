using GalaSoft.MvvmLight;
using Xamarin.Essentials;

namespace MyPassword.ViewModels
{
    public class AboutViewModel:ViewModelBase
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
        
        public string AppName
        {
            get
            {
                return "MyPassword";
            }
        }


        public AboutViewModel()
        {
            var currentVersion = VersionTracking.CurrentVersion;
            AppVersion = currentVersion;
            //var vt = CrossVersionTracking.Current;
            //AppVersion = vt.CurrentVersion;
        }

    }
}
