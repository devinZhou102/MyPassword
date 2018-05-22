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
		public PasswordDetailPage () : base()
        {
            InitializeComponent();
		}

        protected override void OnAppear()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "设置",
                Command = new Command(() =>
                {
                    App.Current.MainPage.Navigation.PushAsync(new SettingPage());
                })
            });
        }

        protected override void OnFirstAppear()
        {
        }
        
        
    }
}