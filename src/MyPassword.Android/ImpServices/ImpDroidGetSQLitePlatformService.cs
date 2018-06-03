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
using MyPassword.IServices;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.Droid.ImpServices.ImpDroidGetSQLitePlatformService))]
namespace MyPassword.Droid.ImpServices
{
    class ImpDroidGetSQLitePlatformService : IGetSQLitePlatformService
    {
        public ISQLitePlatform GetSQLitePlatform()
        {
            if(Build.VERSION.SdkInt >= Build.VERSION_CODES.N)
            {
                return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroidN();
            }
            else
            {
                return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            }
        }
    }
}