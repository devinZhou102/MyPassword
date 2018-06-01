using GalaSoft.MvvmLight.Messaging;
using MyPassword.Pages;
using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobilePlatformApp.Helper
{
    public class MainTabPageHelper
    {
        TabbedPage tabbedPage;
        MainTabViewModel mainTabViewModel;
        public MainTabPageHelper(TabbedPage page,MainTabViewModel viewModel)
        {
            tabbedPage = page;
            mainTabViewModel = viewModel;
            InitData();

            tabbedPage.Navigation.PushModalAsync(new GuestureVerifyPage(()=> { }));
            tabbedPage.Appearing += TabbedPage_Appearing;
            tabbedPage.Disappearing += TabbedPage_Disappearing;
            tabbedPage.CurrentPageChanged += TabbedPage_CurrentPageChanged;
        }

        private void TabbedPage_Disappearing(object sender, EventArgs e)
        {
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            UpdateNavigationBar();
        }

        private void InitData()
        {
            foreach (var page in mainTabViewModel.TabPageList)
            {
                tabbedPage.Children.Add(page);
            }
        }

        private void UpdateNavigationBar()
        {
            tabbedPage.Title = tabbedPage.CurrentPage.Title;
        }

        private void TabbedPage_Appearing(object sender, EventArgs e)
        {
            UpdateNavigationBar();

        }
    }
}
