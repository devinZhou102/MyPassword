using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MyPassword.Dependencys;
using MyPassword.Services;
using SQLite.Net.Cipher.Interfaces;
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
            SimpleIoc.Default.Register<BackUpViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<ChangeSecureKeyViewModel>();
            SimpleIoc.Default.Register<GuestureLockViewModel>();
            SimpleIoc.Default.Register<GuestureVerifyViewModel>();
            SimpleIoc.Default.Register<IconSelectViewModel>();
            SimpleIoc.Default.Register<PasswordEditViewModel>();
            SimpleIoc.Default.Register<PasswordViewModel>();
            SimpleIoc.Default.Register<SecureKeyViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
            SimpleIoc.Default.Register<PwdGenerateViewModel>();
            SimpleIoc.Default.Register<CategorySelectViewModel>();
        }

        private void RegisterDependencys()
        {
            SimpleIoc.Default.Register(() => DependencyService.Get<IGetSQLitePlatformService>());
        }

        private void RegisterServices()
        {
            SimpleIoc.Default.Register<ISecureDatabase, DatabaseService>();
            SimpleIoc.Default.Register<IAlertService, AlertService>();
            SimpleIoc.Default.Register<IGuestureLockService, GuestureLockService>();
            SimpleIoc.Default.Register<ISecureKeyService, SecureKeyService>();
            SimpleIoc.Default.Register<ICategoryService, CategoryService>();
            SimpleIoc.Default.Register<IPwdGenerateService, PwdGenerateService>();
            SimpleIoc.Default.Register<IAppIconService, AppIconService>();
            SimpleIoc.Default.Register<ILoadingService, LoadingService>();
            SimpleIoc.Default.Register<IDataBaseService>(() => DataBaseSerivce.ConnectDataBase(),true);
        }


        public T GetService<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V">参数泛型</typeparam>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public T GetViewModel<T, V>(V param) where T : BaseViewModel
        {
            var vm = ServiceLocator.Current.GetInstance<T>();
            vm.InitializeAsync(param);
            return vm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V">参数泛型</typeparam>
        /// <param name="key"></param>
        /// <param name="param">参数</param>
        /// <param name="CreateViewModel"></param>
        /// <returns></returns>
        public T GetViewModelByKey<T, V>(string key, V param, Func<T> CreateViewModel) where T : BaseViewModel
        {
            T result;
            if (SimpleIoc.Default.IsRegistered<T>(key))
            {
                result = ServiceLocator.Current.GetInstance<T>(key);
            }
            else
            {
                SimpleIoc.Default.Register(() => CreateViewModel, key, true);
                result = SimpleIoc.Default.GetInstance<T>(key);
            }
            result?.InitializeAsync(param);
            return result;
        }

    }
}
