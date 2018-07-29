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
    public partial class MainTabbedPage : TabbedPage
    {
        MainTabViewModel mainTabViewModel;
        public MainTabbedPage ()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);
            mainTabViewModel = new MainTabViewModel();
            InitData();
            this.Appearing += TabbedPage_Appearing;
            this.Disappearing += TabbedPage_Disappearing;
            this.CurrentPageChanged += TabbedPage_CurrentPageChanged;
        }

        private void TabbedPage_Disappearing(object sender, EventArgs e)
        {
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            UpdateNavigationBar();
        }

        private void InitData()
        {
            foreach (var page in mainTabViewModel.TabPageList)
            {
                this.Children.Add(page);
            }
        }

        private void UpdateNavigationBar()
        {
            this.Title = this.CurrentPage.Title;
        }

        private void TabbedPage_Appearing(object sender, EventArgs e)
        {
            UpdateNavigationBar();
        }
    }
}