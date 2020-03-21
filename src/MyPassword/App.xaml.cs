using MyPassword.Helpers;
using MyPassword.Pages;
using MyPassword.Services;
using MyPassword.Themes;
using MyPassword.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyPassword
{
    public partial class App : Application
	{
        private IGuestureLockService GuestureLockService;
        private ISecureKeyService SecureKeyService;

        public App ()
		{
			InitializeComponent();
            Initialize();
            DataBaseHelper.Instance.ConnectDataBase("mypassword");
            MainPage = new InitalPage();
            MainNavi();
        }

        private void Initialize()
        {
            SecureKeyService = Locator.GetService<ISecureKeyService>();
            GuestureLockService = Locator.GetService<IGuestureLockService>();
            ThemeHelper.LightTheme();
        }
        private static readonly ViewModelLocator _locator = new ViewModelLocator();
        public static ViewModelLocator Locator
        {
            get { return _locator; }
        }


        private async void MainNavi()
        {
            await CheckSecureKeyAsync();
            await CheckGuestureLockAsync();
            NaviToMain();
        }

        private Task<bool> CheckSecureKeyAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(async () =>
            {
                await SecureKeyService.LoadSecureKeyAsync();
                if (string.IsNullOrEmpty(SecureKeyService.SecureKey))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MainPage = new MyNavigationPage(new SecureKeyPage(() => {
                            tcs.SetResult(true);
                        }));
                    });
                }
                else
                {
                    tcs.SetResult(true);
                }
            });
            return tcs.Task;
        }

        private Task<bool> CheckGuestureLockAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(GuestureLockService.GuestureLock))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MainPage = new MyNavigationPage(new GuestureLockPage(() =>
                        {
                            tcs.SetResult(true);
                        }));
                    });
                        
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MainPage = new MyNavigationPage(new GuestureVerifyPage(() =>
                        {
                            tcs.SetResult(true);
                        }));
                    });
                }
            });
            return tcs.Task;
        }

        private void NaviToMain()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                switch(Device.RuntimePlatform)
                {
                    case Device.Android:
                    case Device.iOS:
                        MainPage = new AppMainShell();
                        break;
                    case Device.UWP:
                        MainPage = new MyNavigationPage(new MainTabbedPage());
                        break;
                }
            });
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
