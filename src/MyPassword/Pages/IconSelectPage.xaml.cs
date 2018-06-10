using MyPassword.ViewModels;
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
        IconSelectViewModel viewModel;

		public IconSelectPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this,false);
            NavigationPage.SetHasNavigationBar(this,false);
            viewModel = new IconSelectViewModel();
            BindingContext = viewModel;
        }
	}
}