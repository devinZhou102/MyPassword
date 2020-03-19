using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MyPassword.Dependencys;
using MyPassword.Services;
using System;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public class ViewModelLocator
    {


        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            RegisterDependencys();
            RegisterServices();
            RegisterViewModels();
        }


        private void RegisterViewModels()
        {
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<GuestureLockViewModel>(); 
            SimpleIoc.Default.Register<GuestureVerifyViewModel>();
            SimpleIoc.Default.Register<BackUpViewModel>();
            SimpleIoc.Default.Register<IconSelectViewModel>();

        }

        private void RegisterDependencys()
        {
            SimpleIoc.Default.Register(() => DependencyService.Get<IGetSQLitePlatformService>());
        }

        private void RegisterServices()
        {
            SimpleIoc.Default.Register <IAlertService, AlertService>();
        }

        public T GetService<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public T GetViewModel<T>() where T : BaseViewModel
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public T GetViewModelByKey<T>(string key,Func<T> CreateViewModel) where T :BaseViewModel
        {
            if (SimpleIoc.Default.IsRegistered<T>(key))
            {
                return ServiceLocator.Current.GetInstance<T>(key);
            }
            else
            {
                SimpleIoc.Default.Register(() => CreateViewModel,key,true);
                return SimpleIoc.Default.GetInstance<T>(key);
            }
        }

    }
}
