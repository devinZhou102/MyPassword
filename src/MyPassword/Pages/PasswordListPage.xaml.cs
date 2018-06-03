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
                Icon =IconHelper.GetIcon("IconBarAdd"),
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new PasswordDetailPage(()=> {
                        viewModel.LoadData();
                        Navigation.PopAsync();
                    }));
                })
            });
        }

        private async Task ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            if(e.Item is DataItemModel)
            {
               await Navigation.PushAsync(new PasswordDetailPage((e.Item as DataItemModel),() => {
                    viewModel.LoadData();
                    Navigation.PopAsync();
                }));
            }
            await Task.Delay(1000);
           ((ListView)sender).SelectedItem = null;
        }
    }
}