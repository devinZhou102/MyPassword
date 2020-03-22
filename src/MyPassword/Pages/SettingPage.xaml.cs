using MyPassword.Models;
using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            BindingContext = App.Locator.GetViewModel<SettingViewModel, string>("");
            SetTabBarVisible(true);
        }
        

        protected override void OnFirstAppear()
        {
        }

        protected override void OnAppear()
        {
        }

    }
}