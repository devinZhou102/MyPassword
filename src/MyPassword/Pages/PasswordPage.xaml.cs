using MyPassword.Helpers;
using MyPassword.Models;
using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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