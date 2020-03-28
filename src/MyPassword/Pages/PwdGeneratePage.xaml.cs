using MyPassword.ViewModels;
using Rg.Plugins.Popup.Pages;
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
    public partial class PwdGeneratePage : BaseContentPage
    {
        public PwdGeneratePage(Action<string> actionGenerate)
        {
            InitializeComponent();
            BindingContext = App.Locator.GetViewModel<PwdGenerateViewModel,Action<string>>(actionGenerate);
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }
    }
}