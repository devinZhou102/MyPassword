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
    public partial class PopSelectPage : PopupPage
    {
        Action<object> actionSelection;
        public PopSelectPage(List<SelectItem> datas,Action<object> actionSelection)
        {
            InitializeComponent();
            this.actionSelection = actionSelection;
            dataListView.ItemsSource = datas;
        }

        private void DataListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            actionSelection.Invoke((e.Item as SelectItem).Data);
            Device.BeginInvokeOnMainThread(async ()=> {
                await NavigationService.PopPopupAsync();
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await NavigationService.PopPopupAsync();
            });
        }
    }

    public class SelectItem
    {
        public string Text { get; set; }
        
        public  object Data { get; set; }
    }

}