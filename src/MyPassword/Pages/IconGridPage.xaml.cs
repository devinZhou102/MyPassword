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
	public partial class IconGridPage : ContentPage
    {
        IconSelectViewModel viewModel;
        public IconGridPage (Action<string> complete)
		{
			InitializeComponent ();
            viewModel = new IconSelectViewModel(complete);
            BindingContext = viewModel;
        }
	}
}