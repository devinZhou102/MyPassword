using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MyPassword.ViewModels.SecureKeyViewModel;

namespace MyPassword.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecureKeyPage : BaseContentPage
	{
        SecureKeyViewModel viewModel;
		public SecureKeyPage (Action KeySaveEvent)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
            InitViewModel(KeySaveEvent);
        }

        public SecureKeyPage()
        {
            InitializeComponent();
            InitViewModel(null);
        }

        private void InitViewModel(Action KeySaveEvent)
        {
            viewModel = new SecureKeyViewModel(KeySaveEvent);
            BindingContext = viewModel;
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "保存",
                Command = viewModel?.SaveCommand
            });
        }
    }
}