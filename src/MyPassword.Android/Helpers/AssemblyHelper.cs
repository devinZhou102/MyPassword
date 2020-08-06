using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyPassword.Droid.Helpers
{
    public class AssemblyHelper
    {
        public static readonly string ProductVersion
              = typeof(AssemblyHelper).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        public static readonly string CodeVersion
            = typeof(AssemblyHelper).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}