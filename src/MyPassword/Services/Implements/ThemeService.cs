using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;
using MyPassword.Themes;

namespace MyPassword.Services
{
    public class ThemeService : IThemeService
    {
        private const string KEY_THEME_KEY = "themekey";

        public Theme CurrentTheme { get; private set; }

        public string[] ThemeNames;

        public ThemeService()
        {
            ThemeNames = Enum.GetNames(typeof(Theme));
        }

        public async Task LoadThemeAsync()
        {
            var value = await SecureStorage.GetAsync(KEY_THEME_KEY);
            if(string.IsNullOrEmpty(ThemeNames.FirstOrDefault((t)=> t.Equals(value))))
            {
                CurrentTheme = Theme.Light;
            }
            try
            {
                
                CurrentTheme = (Theme)Enum.Parse(typeof(Theme), value);
            }
            catch
            {
                CurrentTheme = Theme.Light;
            }
        }

        public async Task<bool> SaveAsync(Theme theme)
        {
            CurrentTheme = theme;
            await SecureStorage.SetAsync(KEY_THEME_KEY, theme.ToString());
            return true;
        }

        public void ApplyTheme()
        {

            if (CurrentTheme == Theme.Dark)
            {
                ThemeHelper.DarkTheme();
            }
            else
            {
                ThemeHelper.LightTheme();
            }
            
        }
    }

    public enum Theme
    {
        Light = 1,
        Dark = 2
    }

}
