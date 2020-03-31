using GalaSoft.MvvmLight;
using MyPassword.Localization;
using MyPassword.Services;
using System.Threading.Tasks;

namespace MyPassword.ViewModels
{
    public abstract class BaseViewModel:ViewModelBase
    {
        protected IAlertService alertService;
        protected ILoadingService loadingService;

        public BaseViewModel()
        {
            alertService = App.Locator.GetService<IAlertService>();
            loadingService = App.Locator.GetService<ILoadingService>();

        }

        public virtual Task InitializeAsync<T>(T parameter)
        {
            return Task.FromResult(false);
        }
    }
}
