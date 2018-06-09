using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MyPassword.Droid.Helper;
using Plugin.SecureStorage;
using Acr.UserDialogs;
using Plugin.VersionTracking;

namespace MyPassword.Droid
{
    [Activity(Label = "MyPassword", Icon = "@mipmap/icon", Theme = "@style/MainTheme",ScreenOrientation =ScreenOrientation.Portrait, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            BottomBarHelper.SetupBottomTabs(this);
            UserDialogs.Init(this);
            CrossVersionTracking.Current.Track();
            SecureStorageImplementation.StorageType = StorageTypes.AndroidKeyStore;
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

