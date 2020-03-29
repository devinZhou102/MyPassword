using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services
{
    public interface ILoadingService
    { 
        void ShowLoading(string content = "");
        void HideLoading();
    }
}
