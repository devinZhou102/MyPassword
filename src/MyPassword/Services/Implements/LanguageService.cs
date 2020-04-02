using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyPassword.Services
{
    public class LanguageService : ILanguageService
    {

        private const string KEY_LANGUAGE_KEY = "languagekey";
        public CultureInfo CurrentCultureInfo { get; private set; }

        public LanguageService()
        {

        }

        public void ApplyLanguage()
        {
            Device.BeginInvokeOnMainThread(()=>
            {
                Thread.CurrentThread.CurrentCulture = CurrentCultureInfo;
                Thread.CurrentThread.CurrentUICulture = CurrentCultureInfo;
            });
        }

        public async Task LoadLanguageAsync()
        {
            var value = await SecureStorage.GetAsync(KEY_LANGUAGE_KEY);
            if(string.IsNullOrEmpty(value))
            {
                CurrentCultureInfo = CultureInfo.CurrentCulture;
            }
            else
            {
                CurrentCultureInfo = new CultureInfo(value, false);
            }
        }

        public async Task<bool> SaveAsync(string language)
        {
            CurrentCultureInfo = new CultureInfo(language, false);
            await SecureStorage.SetAsync(KEY_LANGUAGE_KEY, language);
            return true;
        }
    }
}
