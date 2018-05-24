using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        static int count = 0;
		public PasswordDetailPage () : base()
        {
            InitializeComponent();
            count++;

        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "保存",
                Command = new Command(() =>
                {
                })
            });
        }

        public override void OnPoppedOut()
        {
            count--;
            Debug.WriteLine(string.Format("{0} is popped out,has {1} instance", this.GetType().Name,count));
        }
    }
}