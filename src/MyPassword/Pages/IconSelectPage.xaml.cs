using MyPassword.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
	public partial class IconSelectPage : PopupPage
    {
		public IconSelectPage (Action<string> complete)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this,false);
            NavigationPage.SetHasNavigationBar(this,false);
            var vm = App.Locator.GetViewModel<IconSelectViewModel>();
            vm.SelectIconComplete = complete;
            BindingContext = vm;
        }
        

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();
            return false;
        }


        private async void CloseAllPopup()
        {
            await NavigationService.PopPopupAsync();
        }
    }
}