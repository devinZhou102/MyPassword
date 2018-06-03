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
	public partial class GuestureLockPage : BaseContentPage
	{
        GusetureLockViewModel viewModel;
        public GuestureLockPage()
        {
            InitializeComponent();
            InitViewModel(()=> {
                Navigation.PopAsync();
            });
        }

        public GuestureLockPage(Action ActionSetLockFinish)
        {
            InitializeComponent();
            InitViewModel(ActionSetLockFinish);
        }

        private void InitViewModel(Action ActionSetLockFinish)
        {

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            viewModel = new GusetureLockViewModel(ActionSetLockFinish);
            BindingContext = viewModel;
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }
    }
}