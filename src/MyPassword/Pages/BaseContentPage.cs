using MyPassword.Interface;
using MyPassword.Themes;
using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyPassword.Pages
{
    public abstract class BaseContentPage : ContentPage, IPoppedOut
    {
        private bool IsFirstAppear;

        public BaseContentPage()
        {
            IsFirstAppear = true;
            this.Appearing += BaseContentPage_Appearing;
            this.Disappearing += BaseContentPage_Disappearing;
            SetTabBarVisible(false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void BaseContentPage_Disappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<ThemeMessage>(this, ThemeMessage.ThemeChanged);
        }

        public void SetTabBarVisible(bool visible)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                case Device.iOS:
                    Shell.SetTabBarIsVisible(this, visible);
                    break;
            }

        }

        private void BaseContentPage_Appearing(object sender, EventArgs e)
        {
            if (IsFirstAppear)
            {
                OnFirstAppear();
                IsFirstAppear = false;
            }
            else
            {
                OnAppear();
            }

            MessagingCenter.Subscribe<ThemeMessage>(this, ThemeMessage.ThemeChanged, (tm) => UpdateTheme(tm));
        }

        public void UpdateTheme(ThemeMessage tm)
        {

        }

        #region abstract method

        protected abstract void OnFirstAppear();

        protected abstract void OnAppear();

        public virtual void OnPoppedOut()
        {
            Debug.WriteLine(string.Format("{0} is popped out",this.GetType().Name));
        }


        #endregion
    }
}