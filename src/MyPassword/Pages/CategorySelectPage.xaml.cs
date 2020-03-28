using MyPassword.Models;
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
    public partial class CategorySelectPage : BaseContentPage
    {
        public CategorySelectPage(Action<CategoryModel> actionSelect)
        {
            InitializeComponent();
            BindingContext = App.Locator.GetViewModel<CategorySelectViewModel, Action<CategoryModel>>(actionSelect);
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }
    }
}