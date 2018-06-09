using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyPassword
{
    public class NavigationService
    {

        public static INavigation Navigation { get { return App.Current.MainPage.Navigation; } }

        public static void PopAsync()
        {
            Navigation?.PopAsync();
        }

        public static void PopModalAsync()
        {
            Navigation?.PopModalAsync();
        }
    }
}
