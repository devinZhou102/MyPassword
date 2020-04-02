using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Localization;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private ObservableCollection<SettingItemModel> _SettingItemList;

        public ObservableCollection<SettingItemModel> SettingItemList
        {
            get
            {
                if (_SettingItemList == null)
                {
                    _SettingItemList = new ObservableCollection<SettingItemModel>();
                }
                return _SettingItemList;
            }
            set
            {
                _SettingItemList = value;
                RaisePropertyChanged(nameof(SettingItemList));
            }
        }

        private IThemeService themeService;
        private ILanguageService languageService;
        public SettingViewModel(IThemeService themeService, ILanguageService languageService)
        {
            this.themeService = themeService;
            this.languageService = languageService;
            InitSettingItemList();
        }

        public ICommand TappedCommand => new RelayCommand<SettingItemModel>(async (item) =>
        {
            if (item.SecureProtect)
            {
                await NavigationService.PushModalAsync(new GuestureVerifyPage(async () =>
                {
                    PushPage(item);
                    await NavigationService.PopModalAsync();
                }, true));
            }
            else
            {
                PushPage(item);
            }

        });

        private void PushPage(SettingItemModel item)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var paramTpyes = new Type[0];
                var constructor = item.PageType.GetConstructor(paramTpyes);
                if (constructor != null)
                {
                    var page = constructor.Invoke(null) as Page;
                    await NavigationService.PushAsync(page);
                }
            });
        }

        private void InitSettingItemList()
        {
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Key,
                Title = AppResource.ItemSecureKey,
                Description = AppResource.ItemSecureKeyDesc,
                SecureProtect = true,
                Background = "#7373B9",
                PageType = typeof(ChangeSecureKeyPage),
                TappedCommand = TappedCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Lock,
                Title = AppResource.ItemGuesture,
                SecureProtect = true,
                Description = AppResource.ItemGuestureDesc,
                Background = "#9F4D95",
                PageType = typeof(GuestureLockPage),
                TappedCommand = TappedCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Backup,
                SecureProtect = true,
                Title = AppResource.ItemBackup,
                Description = AppResource.ItemBackupDesc,
                Background = "#949449",
                PageType = typeof(BackUpPage),
                TappedCommand = TappedCommand

            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Language,
                Title = AppResource.ItemLanguage,
                SecureProtect = false,
                Description = AppResource.ItemLanguageDesc,
                Background = "#00DB00",
                TappedCommand = LanguageCommand
            }); 
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.Theme,
                Title = AppResource.ItemTheme,
                SecureProtect = false,
                Description = AppResource.ItemThemeDesc,
                Background = "#B15BFF",
                TappedCommand = ThemeCommand
            });
            SettingItemList.Add(new SettingItemModel
            {
                Icon = IconFont.About,
                Title = AppResource.ItemAbout,
                SecureProtect = false,
                Description = AppResource.ItemAboutDesc,
                Background = "#009393",
                PageType = typeof(AboutPage),
                TappedCommand = TappedCommand
            });
        }

        public ICommand LanguageCommand => new RelayCommand(async ()=> 
        {
            var list = new List<SelectItem>
            {
                new SelectItem
                {
                    Text = AppResource.LanguageCN,
                    Data = "zh-cn"
                },
                new SelectItem
                {
                    Text = AppResource.LanguageEN,
                    Data = "en-us"
                }

            };

            await NavigationService.PushPopupPageAsync(new PopSelectPage(AppResource.TitleThemeSelect, list, async (data) =>
            {

                Device.BeginInvokeOnMainThread(async ()=>
                {
                    await languageService.SaveAsync(data.ToString());
                    languageService.ApplyLanguage();
                });
            }));
        });

        public ICommand ThemeCommand => new RelayCommand(async () => 
        {
            var list = new List<SelectItem>
            {
                new SelectItem
                {
                    Text = AppResource.LightTheme,
                    Data = Theme.Light
                },
                new SelectItem
                {
                    Text = AppResource.DarkTheme,
                    Data = Theme.Dark
                }

            };
            await NavigationService.PushPopupPageAsync(new PopSelectPage(AppResource.TitleThemeSelect, list, async (data)=> 
            {
                if (data is Theme theme)
                {
                    var result = await themeService.SaveAsync(theme);
                    Device.BeginInvokeOnMainThread(()=> {
                        if (result) themeService.ApplyTheme();
                    });
                }
            }));
        });

    }
}
