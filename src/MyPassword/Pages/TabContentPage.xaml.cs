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
	public partial class TabContentPage : ContentPage
	{


        public const int TAB_PWD = 1;//聊天
        public const int TAB_SET = 2;//通讯录

        public int TabType { get; set; }

        public TabContentPage ()
		{
			InitializeComponent ();
		}
	}
}