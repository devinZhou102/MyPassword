using System.Threading.Tasks;

namespace MyPassword.Services
{
    public interface IAlertService
    {
        void DisplayAlert(string title, string content, string actionName);

        Task DisplayAlertAsync(string title, string content, string actionName);

        Task<bool> ConfirmAlertAsync(string message, string title = null, string okText = null, string cancelText = null);

        void Toast(string content);

    }
}