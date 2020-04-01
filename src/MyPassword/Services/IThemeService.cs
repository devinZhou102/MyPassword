using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public interface IThemeService
    {

        Theme CurrentTheme { get; }

        Task LoadThemeAsync();

        Task<bool> SaveAsync(Theme value);

        void ApplyTheme();
    }
}
