using MyPassword.Models;
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
	public partial class PasswordEditPage : BaseContentPage
	{
        PasswordEditViewModel viewModel;
		public PasswordEditPage() 
        {
            InitializeComponent();
            InitViewModel(null);
            Title = "新增";
        }

        public PasswordEditPage(DataItemModel dataItem)
        {
            Title = "修改";
            InitializeComponent();
            InitViewModel(dataItem);
        }

        private void InitViewModel(DataItemModel dataItem)
        {
            viewModel = App.Locator.GetViewModel<PasswordEditViewModel, DataItemModel>(dataItem);
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
                Command = viewModel?.SaveCommand
            });
        }

        public override void OnPoppedOut()
        {
            Debug.WriteLine(string.Format("{0} is popped out", this.GetType().Name));
        }
    }
}