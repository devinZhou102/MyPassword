﻿using MyPassword.ViewModels;
using Xamarin.Forms.Xaml;

namespace MyPassword.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordPage : BaseContentPage
	{

        PasswordViewModel viewModel;
		public PasswordPage()
		{
			InitializeComponent ();
            SetTabBarVisible(true);
            InitViewModel();
        }

        public PasswordPage(string categoryKey)
        {
            InitializeComponent();
            SetTabBarVisible(true);
            InitViewModel(categoryKey);
        }

        private void InitViewModel(string key = "")
        {
            viewModel = App.Locator.GetViewModel<PasswordViewModel, string>(key);
            BindingContext = viewModel;
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }


        public override void OnPoppedOut()
        {
            base.OnPoppedOut();
            viewModel?.Cleanup();
        }
    }
}