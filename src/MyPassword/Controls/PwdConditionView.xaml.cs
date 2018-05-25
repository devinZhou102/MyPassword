using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyPassword.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PwdConditionView : ContentView
	{




        PwdConditinoViewModel ViewModel;
		public PwdConditionView ()
		{
			InitializeComponent ();
            ViewModel = new PwdConditinoViewModel();
            BindingContext = ViewModel;
		}
	}
}