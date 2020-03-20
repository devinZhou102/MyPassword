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

        public static INavigation Navigation { get { return App.Current.MainPage.Navigation; } }

        public static void PushAsync(Page page)
        {
            Navigation.PushAsync(page);
        }

        public static void PopAsync()
        {
            Navigation?.PopAsync();
        }

        public static void PushModalAsync(Page page)
        {
            Navigation.PushModalAsync(page);
        }

        public static void PopModalAsync()
        {
            Navigation?.PopModalAsync();
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
