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
	public partial class PasswordListPage : BaseContentPage
	{

        PasswordListViewModel viewModel;
		public PasswordListPage()
		{
			InitializeComponent ();
            SetTabBarVisible(true);
            InitViewModel();
        }

        public PasswordListPage(string categoryKey)
        {
            InitializeComponent();
            SetTabBarVisible(true);
            InitViewModel(categoryKey);
        }

        private void InitViewModel(string key = "")
        {
            viewModel = App.Locator.GetViewModel<PasswordListViewModel, string>(key);
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