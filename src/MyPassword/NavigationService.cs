using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPassword
{
    public class NavigationService
    {

        static INavigation Navigation { get { return App.Current.MainPage.Navigation; } }

        public static async Task PushAsync(Page page)
        {
           await Navigation.PushAsync(page);
        }

        public static async Task PopAsync()
        {
            await Navigation?.PopAsync();
        }

        public static async Task PushModalAsync(Page page)
        {
            await Navigation.PushModalAsync(page);
        }

        public static async Task PopModalAsync()
        {
            await Navigation?.PopModalAsync();
        }


        public static async Task PushPopupPageAsync(PopupPage popup, bool animated = true)
        {
            await PopupNavigation.Instance.PushAsync(popup, animated);
        }

        public static async Task PopPopupAsync(bool animated = true)
        {
            await PopupNavigation.Instance.PopAsync(animated);
        }
    }
}
