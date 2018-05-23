using MyPassword.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPassword.Pages
{
	public partial class MainPage : BaseContentPage
    {
		public MainPage()
		{
			InitializeComponent();
		}
        

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PasswordDetailPage());
        }

        protected override void OnFirstAppear()
        {
            ToolbarItems.Add(new ToolbarItem {
                Text = "设置",
                Command = new Command(() =>
                {
                    App.Current.MainPage.Navigation.PushAsync(new SettingPage());
                })
            });
        }

        protected override void OnAppear()
        {
        }
    }
}
