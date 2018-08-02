using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class BackUpViewModel:ViewModelBase
    {

        public BackUpViewModel()
        {
            ExportDataCommand = new RelayCommand(() => ExportDataExcute());
            InportDataCommand = new RelayCommand(() => InportDataExcute());
        }


        private void ExportDataExcute()
        {
            CreateGraphClientAsync();
        }

        private void InportDataExcute()
        {

        }

        public ICommand ExportDataCommand { get; private set; }
        public ICommand InportDataCommand { get; private set; }

        private async Task<bool> CreateGraphClientAsync()
        {
            try
            {
               var Client = new GraphServiceClient("https://graph.microsoft.com/v1.0",
                         new DelegateAuthenticationProvider(
                         async(requestMessage) =>
                {
                    var tokenRequest = await App.IdentityClientApp.AcquireTokenAsync(App.Scopes, App.UiParent).ConfigureAwait(false);
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", tokenRequest.AccessToken);
                }));
                var Me = await Client.Me.Request().GetAsync();
                //Username.Text = $"Welcome {((User)Me).DisplayName}";
                return true;
            }
            catch (MsalException ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK", "Cancel");
                return false;
            }
        }
    }
}
