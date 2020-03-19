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
        GuestureLockViewModel viewModel;
        public GuestureLockPage()
        {
            InitializeComponent();
            InitViewModel(()=> {
                Navigation.PopAsync();
            });
            Title = "重设手势密码";
        }

        public GuestureLockPage(Action ActionSetLockFinish)
        {
            InitializeComponent();
            InitViewModel(ActionSetLockFinish);
            Title = "手势密码";
        }

        private void InitViewModel(Action actionSetLockFinish)
        {
            viewModel = App.Locator.GetViewModel<GuestureLockViewModel>();
            viewModel.ActionSetLockFinish = actionSetLockFinish;
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