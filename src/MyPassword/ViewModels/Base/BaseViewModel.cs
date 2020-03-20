using GalaSoft.MvvmLight;
using MyPassword.Localization;
using MyPassword.Services;
using System.Threading.Tasks;

namespace MyPassword.ViewModels
{
    public abstract class BaseViewModel:ViewModelBase
    {
        protected IAlertService alertService;

        public BaseViewModel()
        {
            alertService = App.Locator.GetService<IAlertService>();

        }

        public virtual Task InitializeAsync<T>(T parameter)
        {
            return Task.FromResult(false);
        }
    }
}
