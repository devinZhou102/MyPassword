using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyPassword
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppMainShell : Shell
    {
        public AppMainShell()
        {
            InitializeComponent();
            this.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}