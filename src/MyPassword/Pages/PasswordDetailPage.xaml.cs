using MyPassword.Models;
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
	public partial class PasswordDetailPage : BaseContentPage
	{
        PasswordDetailViewModel viewModel;
        public PasswordDetailPage (DataItemModel dataItem)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this,true);
            viewModel = new PasswordDetailViewModel(dataItem);
            BindingContext = viewModel;
		}

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "删除",
                Command = viewModel?.DeleteCommand
            });
        }

        public override void OnPoppedOut()
        {
            base.OnPoppedOut();
            viewModel?.Cleanup();
        }
    }
}