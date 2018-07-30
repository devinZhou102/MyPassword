using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyPassword.Droid
{
    [Activity(Label = "MyPassword", Icon = "@mipmap/icon", Theme = "@style/SplashStyle",MainLauncher =true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        Context Context;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Context = this;
            SetContentView(Resource.Layout.LayoutSplash);
            GotoMain();
        }

        private void GotoMain()
        {
            new Handler().PostDelayed(
               () => {
                   Context.StartActivity(typeof(MainActivity));
                   Finish();
               }
               , 2000);
        }
    }
}