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
	public partial class SettingPage : BaseContentPage
	{
		public SettingPage () : base()
        {
            InitializeComponent();
		}
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MyNavigationPage(new MainPage());
        }

        protected override void OnFirstAppear()
        {
            ToolbarItems.Add(new ToolbarItem { Text = "收藏" });
        }

        protected override void OnAppear()
        {
        }
    }
}