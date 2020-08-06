using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyPassword.Dependencys;
using MyPassword.Droid.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.Droid.Dependencys.ImpDroidAssemblyInfoService))]
namespace MyPassword.Droid.Dependencys
{
    public class ImpDroidAssemblyInfoService : IAssemblyInfoService
    {
        public string GetCodeVersion()
        {
            return AssemblyHelper.CodeVersion;
        }

        public string GetProductVersion()
        {
            return AssemblyHelper.ProductVersion;
        }
    }
}