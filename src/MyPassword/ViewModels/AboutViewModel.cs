using GalaSoft.MvvmLight;
using Plugin.VersionTracking;
using System;
using System.Collections.Generic;
using System.Text;

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
            var vt = CrossVersionTracking.Current;
            AppVersion = vt.CurrentVersion;
            
        }

    }
}
