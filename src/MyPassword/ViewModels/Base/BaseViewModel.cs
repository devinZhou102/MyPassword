using GalaSoft.MvvmLight;
using MyPassword.Localization;
using MyPassword.Services;

namespace MyPassword.ViewModels
{
    public abstract class BaseViewModel:ViewModelBase
    {
        protected IAlertService alertService;

        public BaseViewModel()
        {
            alertService = App.Locator.GetService<IAlertService>();

        }

    }
}
