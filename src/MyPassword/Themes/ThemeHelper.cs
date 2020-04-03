using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyPassword.Themes
{
    public static class ThemeHelper
    {
        private static void ChangeTheme(string theme)
        {
            //// clear all the resources
            //Application.Current.Resources.MergedDictionaries.Clear();
            //Application.Current.Resources.Clear();
            ResourceDictionary applicationResourceDictionary = Application.Current.Resources;
            ResourceDictionary newTheme = null;

            switch (theme.ToLowerInvariant())
            {
                case "light":
                    newTheme = new LightTheme();
                    break;
                case "dark":
                    newTheme = new DarkTheme();
                    break;
                    
            }

            foreach (var merged in newTheme.MergedDictionaries)
            {
                applicationResourceDictionary.MergedDictionaries.Add(merged);
            }

            ManuallyCopyThemes(newTheme, applicationResourceDictionary);
            MessagingCenter.Send<ThemeMessage>(new ThemeMessage(), ThemeMessage.ThemeChanged);

        }

        public static void LightTheme()
        {
            ChangeTheme("light");
        }

        public static void DarkTheme()
        {
            ChangeTheme("dark");
        }

        private static void ManuallyCopyThemes(ResourceDictionary fromResource, ResourceDictionary toResource)
        {
            foreach (var item in fromResource.Keys)
            {
                toResource[item] = fromResource[item];
            }
        }
    }
}
