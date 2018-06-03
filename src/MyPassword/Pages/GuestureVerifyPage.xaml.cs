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
        
        public GuestureVerifyPage(Action successedEvent)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this,false);
            BindingContext = new GuestureVerifyViewModel(successedEvent);
		}

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }
    }
}