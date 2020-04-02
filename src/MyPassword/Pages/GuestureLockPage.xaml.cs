using MyPassword.Localization;
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
        GuestureLockViewModel viewModel;
        public GuestureLockPage()
        {
            InitializeComponent();
            InitViewModel(()=> {
                Navigation.PopAsync();
            });
            Title = AppResource.TitleResetGuestureLock;
        }

        public GuestureLockPage(Action ActionSetLockFinish)
        {
            InitializeComponent();
            InitViewModel(ActionSetLockFinish);
            Title = AppResource.TitleGuestureLock;
        }

        private void InitViewModel(Action actionSetLockFinish)
        {
            viewModel = App.Locator.GetViewModel<GuestureLockViewModel,Action>(actionSetLockFinish);
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