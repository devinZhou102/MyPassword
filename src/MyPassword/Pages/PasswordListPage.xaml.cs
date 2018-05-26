using MyPassword.Helpers;
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
		public PasswordListPage ()
		{
			InitializeComponent ();
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
                    Navigation.PushAsync(new PasswordDetailPage());
                })
            });
        }
    }
}