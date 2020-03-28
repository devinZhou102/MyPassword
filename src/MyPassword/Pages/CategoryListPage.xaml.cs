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
    public partial class CategoryListPage : BaseContentPage
    {
        CategoryViewModel viewModel;
        public CategoryListPage()
        {
            InitializeComponent();
            viewModel = App.Locator.GetViewModel<CategoryViewModel,string>("");
            BindingContext = viewModel;
            SetTabBarVisible(true);
        }

        protected override void OnAppear()
        {
            viewModel.UpdateCategory();
        }

        protected override void OnFirstAppear()
        {
            viewModel.UpdateCategory();
        }
    }
}