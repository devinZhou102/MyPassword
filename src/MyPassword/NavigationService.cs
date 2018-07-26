using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
