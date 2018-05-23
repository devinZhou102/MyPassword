using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyPassword.Helpers
{
    public class IconHelper
    {

        public static string GetIcon(string icon)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    return string.Format("Assets/{0}.png", icon);
                default:
                    return icon;
            }
        }
    }
}
