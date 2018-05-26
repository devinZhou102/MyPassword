using MyPassword.ViewModels;
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
        PasswordDetailViewModel viewModel;
		public PasswordDetailPage () : base()
        {
            InitializeComponent();
            viewModel = new PasswordDetailViewModel();
            BindingContext = viewModel;

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
            Debug.WriteLine(string.Format("{0} is popped out", this.GetType().Name));
        }
    }
}