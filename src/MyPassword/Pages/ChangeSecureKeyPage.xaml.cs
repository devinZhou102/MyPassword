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
	public partial class ChangeSecureKeyPage : BaseContentPage
	{
        ChangeSecureKeyViewModel viewModel;

		public ChangeSecureKeyPage ()
		{
			InitializeComponent ();
            viewModel = App.Locator.GetViewModel<ChangeSecureKeyViewModel,string>("");
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