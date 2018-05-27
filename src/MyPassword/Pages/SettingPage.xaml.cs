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
	public partial class SettingPage : BaseContentPage
	{
        SettingViewModel viewModel;
		public SettingPage () : base()
        {
            InitializeComponent();
            viewModel = new SettingViewModel();
            BindingContext = viewModel;

        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
        }

        protected override void OnFirstAppear()
        {
        }

        protected override void OnAppear()
        {
        }
    }
}