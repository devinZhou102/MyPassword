using Acr.UserDialogs;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public class AlertService : IAlertService
    {
        public void DisplayAlert(string title, string content, string actionName)
        {
            UserDialogs.Instance.Alert(content, title, actionName);
        }

        public async Task<bool> ConfirmAlertAsync(string message, string title = null, string okText = null, string cancelText = null)
        {
            return await UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText);
        }

        public async Task DisplayAlertAsync(string title, string content, string actionName)
        {
            await UserDialogs.Instance.AlertAsync(content, title, actionName);
        }

        public void Toast(string content)
        {
            UserDialogs.Instance.Toast(content);
        }
    }
}
