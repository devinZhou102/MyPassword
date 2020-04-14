using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MyPassword.ViewModels.GuestureVerifyViewModel;

namespace MyPassword.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuestureVerifyPage : BaseContentPage
	{
        readonly GuestureVerifyViewModel ViewModel;
        public GuestureVerifyPage(Action successedEvent,bool closeButtonVisible = false)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this,false);
            ViewModel = App.Locator.GetViewModel<GuestureVerifyViewModel,Action>(successedEvent);
            BindingContext = ViewModel;
            ButtonClose.IsVisible = closeButtonVisible; 
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void ButtonClose_ClickedAsync(object sender, EventArgs e)
        {
            await NavigationService.PopModalAsync();
            SetResumeCheckWorkable();
        }

        private void SetResumeCheckWorkable()
        {
            (App.Current as App).IsResumeCheckWorkable = true;
        }

        protected override bool OnBackButtonPressed()
        {
            SetResumeCheckWorkable();
            return base.OnBackButtonPressed();
        }
    }
}