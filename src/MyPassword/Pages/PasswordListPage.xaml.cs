using MyPassword.Helpers;
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
	public partial class PasswordListPage : BaseContentPage
	{

        PasswordListViewModel viewModel;
		public PasswordListPage ()
		{
			InitializeComponent ();
            viewModel = new PasswordListViewModel();
            BindingContext = viewModel;

        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
            ToolbarItems.Add(new ToolbarItem {
                Text = "添加" ,
                IconImageSource = IconHelper.GetIcon("IconBarAdd"),
                Command = viewModel?.AddDataCommand
            });
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            if(e.Item is DataItemModel)
            {
                Device.BeginInvokeOnMainThread(async ()=> {
                    await Navigation.PushAsync(new PasswordDetailPage((e.Item as DataItemModel)));
                });
            }
            //await Task.Delay(1000);
           ((ListView)sender).SelectedItem = null;
        }

        public override void OnPoppedOut()
        {
            base.OnPoppedOut();
            viewModel?.Cleanup();
        }
    }
}