using MobilePlatformApp.Helper;
using MyPassword.ViewModels;
using Naxam.Controls.Forms;
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
	public partial class MainTabbedDroidPage : BottomTabbedPage
    {

		public MainTabbedDroidPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);
            new MainTabPageHelper(this, new MainTabViewModel());
        }
	}
}