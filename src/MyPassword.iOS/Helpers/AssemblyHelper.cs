using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Foundation;
using UIKit;

namespace MyPassword.iOS.Helpers
{
    public class AssemblyHelper
    {
        public static readonly string ProductVersion
              = typeof(AssemblyHelper).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        public static readonly string CodeVersion
              = typeof(AssemblyHelper).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}