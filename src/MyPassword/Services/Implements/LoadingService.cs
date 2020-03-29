using Acr.UserDialogs;

namespace MyPassword.Services
{
    public class LoadingService : ILoadingService
    {
        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        public void ShowLoading(string content = "")
        {
            UserDialogs.Instance.ShowLoading(content);
        }
    }
}
