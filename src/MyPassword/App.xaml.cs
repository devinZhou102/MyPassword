using Microsoft.Identity.Client;
using MyPassword.Helpers;
using MyPassword.Manager;
using MyPassword.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MyPassword
{
	public partial class App : Application
	{

        public static PublicClientApplication IdentityClientApp = null;
        public static string ClientID = "1352d1c0-42ff-4a1e-90b0-c74e691bde2e";
        public static string[] Scopes = { "User.Read", "User.ReadBasic.All", "Files.Read", "Files.Read.All", "Files.ReadWrite", "Files.ReadWrite.All" };
        public static UIParent UiParent = null;

        public App ()
		{
			InitializeComponent();
            DataBaseHelper.Instance.ConnectDataBase("mypassword");
            MainPage = new ContentPage();
            IdentityClientApp = new PublicClientApplication(ClientID);
            MainNavi();
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
            Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(SecureKeyManager.Instance.SecureKey))
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
                if (string.IsNullOrEmpty(LockManager.Instance.GuestureLock))
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
                MainPage = new MyNavigationPage(new MainTabbedPage());
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
