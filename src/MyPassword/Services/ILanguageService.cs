using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public interface ILanguageService
    {

        CultureInfo CurrentCultureInfo { get; }

        Task LoadLanguageAsync();

        Task<bool> SaveAsync(string language);

        void ApplyLanguage();
    }
}
