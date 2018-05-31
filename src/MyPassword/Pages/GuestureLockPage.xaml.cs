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
	public partial class GuestureLockPage : BaseContentPage
	{
        GusetureLockViewModel viewModel;
        public GuestureLockPage ()
		{
            InitializeComponent();
            viewModel = new GusetureLockViewModel();
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