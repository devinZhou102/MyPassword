using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Const;
using MyPassword.Dtos;
using MyPassword.Localization;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordEditViewModel : BaseViewModel
    {

        private string _Password;

        public string Password
        {
            get
            {
                if (null == _Password)
                {
                    _Password = "";
                }
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private string _Title;
        public string Title
        {
            get
            {
                if (null == _Title)
                {
                    _Title = "";
                }
                return _Title;
            }
            set
            {
                _Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private string _Account;
        public string Account
        {
            get
            {
                if (null == _Account)
                {
                    _Account = "";
                }
                return _Account;
            }
            set
            {
                _Account = value;
                RaisePropertyChanged(nameof(Account));
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                if (_Description == null)
                {
                    _Description = "";
                }
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private string _Icon;
        public string Icon
        {
            get
            {
                if (null == _Icon)
                {
                    _Icon = "";
                }
                return _Icon;
            }
            set
            {
                _Icon = value;
                RaisePropertyChanged(nameof(Icon));
            }
        }

        private string _CategoryIcon;
        public string CategoryIcon
        {
            get => _CategoryIcon ?? (_CategoryIcon = "");
            set
            {
                _CategoryIcon = value;
                RaisePropertyChanged(nameof(CategoryIcon));
            }
        }

        private string _CategoryName;
        public string CategoryName
        {
            get => _CategoryName ?? (_CategoryName = "");
            set
            {
                _CategoryName = value;
                RaisePropertyChanged(nameof(CategoryName));
            }
        }

        private string _CategoryBackground;
        public string CategoryBackground
        {
            get => _CategoryBackground ?? (_CategoryBackground = "");
            set
            {
                _CategoryBackground = value;
                RaisePropertyChanged(nameof(CategoryBackground));
            }
        }

        private string CategoryKey;

        private string _FontIconSource;
        public string FontIconSource
        {
            get => _FontIconSource ?? (_FontIconSource = "");
            set
            {
                _FontIconSource = value;
                RaisePropertyChanged(nameof(FontIconSource));
            }
        }

        private string _FontIconBg;
        public string FontIconBg
        {
            get => _FontIconBg ?? (_FontIconBg = "#9F35FF");
            set
            {
                _FontIconBg = value;
                RaisePropertyChanged(nameof(FontIconBg));
            }
        }

        private string _Website;
        public string Website
        {
            get => _Website ?? (_Website = "");
            set
            {
                _Website = value;
                RaisePropertyChanged(nameof(Website));
            }
        }

        private string _Phone;
        public string Phone
        {
            get => _Phone ?? (_Phone = "");
            set
            {
                _Phone = value;
                RaisePropertyChanged(nameof(Phone));
            }
        }

        private DateTimeOffset _UpdateTime;
        public DateTimeOffset UpdateTime
        {
            get => _UpdateTime;
            set
            {
                _UpdateTime = value;
                RaisePropertyChanged(nameof(UpdateTime));
            }
        }

        private bool _CategoryEditable;
        public bool CategoryEditable
        {
            get => _CategoryEditable;
            set
            {
                _CategoryEditable = value;
                RaisePropertyChanged(nameof(CategoryEditable));
            }
        }

        DataItemModel DataItem;
        private readonly ISecureKeyService secureKeyService;
        private readonly ICategoryService categoryService;
        private IDataBaseService secureDatabase;

        public PasswordEditViewModel(IDataBaseService secureDatabase, ISecureKeyService secureKeyService,ICategoryService categoryService)
        {
            this.secureDatabase = secureDatabase;
            this.secureKeyService = secureKeyService;
            this.categoryService = categoryService;
          
        }

        public override Task InitializeAsync<T>(T parameter)
        {

            if (parameter is PwdDataDto data)
            {
                InitDatas(data);
            }
            else
            {
                InitDatas(null);
            }
            return base.InitializeAsync(parameter);
        }

        private void InitDataItem(DataItemModel data)
        {
            DataItem = data;
            Icon = DataItem?.Icon;
            Account = DataItem?.Account;
            Title = DataItem?.Name;
            Password = DataItem?.Password;
            Description = DataItem?.Description;
            Phone = DataItem?.Phone;
            Website = DataItem?.Website;
            UpdateTime = DataItem == null ? DateTimeOffset.UtcNow : DataItem.UpdateTime;
        }

        private void InitCategory(string categoryKey)
        {
            CategoryKey = categoryKey;
            if (string.IsNullOrEmpty(CategoryKey))
            {
                var c = categoryService.GetDefaultCategory();
                UpdateCategory(c);
            }
            else
            {
                var c = categoryService.FindCategoryByKey(CategoryKey);
                UpdateCategory(c);
            }
            var icon = FontIcon.ToFontIcon(Icon);
            UpdateFontIcon(icon);
        }

        private void InitDatas(PwdDataDto data)
        {
            if (data == null || (string.IsNullOrEmpty(data.CategoryKey) && data.Data == null) )// new 
            {
                InitCategory("");
                InitDataItem(null);
                CategoryEditable = true;
            }
            else if (data.Data == null && !string.IsNullOrEmpty(data.CategoryKey))// new with category
            {
                InitCategory(data.CategoryKey);
                InitDataItem(null);
                CategoryEditable = false;
            }
            else if (string.IsNullOrEmpty(data.CategoryKey) && data.Data != null)// edit
            {
                InitCategory(data.Data.CategoryKey);
                InitDataItem(data.Data);
                CategoryEditable = true;
            }
        }


        private void UpdateCategory(CategoryModel category)
        {
            CategoryName = category.Name;
            CategoryIcon = category.Icon;
            CategoryKey = category.Key;
            CategoryBackground = category.Background;
        }

        public ICommand SaveCommand => new RelayCommand(async ()=> await SaveExcuteAsync());

        public ICommand GenerateCommand => new RelayCommand(async () => await GenerateExcuteAsync());

        public ICommand ImageTapCommand => new RelayCommand(async () => await ImageTapExcuteAsync());

        public ICommand CategoryCommand => new RelayCommand(async () => 
        {
            if(CategoryEditable)
                await NavigationService.PushAsync(new CategorySelectPage((c) => UpdateCategory(c)));
        });

        private async Task SaveExcuteAsync()
        {

            if (!IsValid())
            {
                return;
            }

            var dialog = UserDialogs.Instance.Loading("数据保存中...");
            var success = await SavePassword();
            if (success != null)
            {
                UserDialogs.Instance.Toast("保存数据成功");
                MessengerInstance.Send<int>(TokenConst.TokenUpdateList);
                await NavigationService.PopAsync();
            }
            else
            {
                UserDialogs.Instance.Toast("保存数据失败");
            }
            dialog.Hide();
        }

        private DataItemModel GetDataItemModel(int id)
        {
            var item = new DataItemModel
            {
                Id = id,
                Icon = Icon,
                Account = Account,
                Password = Password,
                Name = Title,
                CategoryKey = CategoryKey,
                Phone = Phone,
                UpdateTime = DateTimeOffset.UtcNow,
                Website = Website,
                Description = Description
            };
            return item;
        }

        private bool IsValid()
        {
            bool isValid = true;
            string msg = "";
            if (string.IsNullOrEmpty(Account.Trim()))
            {
                isValid = false;
                msg = AppResource.MsgAccount;
            }
            else if (string.IsNullOrEmpty(Password.Trim()))
            {
                isValid = false;
                msg = AppResource.MsgPassword;
            }
            else if (string.IsNullOrEmpty(Title.Trim()))
            {
                isValid = false;
                msg = AppResource.MsgTitle;
            }

            if (!isValid)
            {
                alertService.DisplayAlert("",msg,AppResource.DialogButtonConfirm);
            }
            return isValid;
        }

        private Task<DataItemModel> SavePassword()
        {
            var tcs = new TaskCompletionSource<DataItemModel>();
            Task.Factory.StartNew(() =>
            {
                var item = new DataItemModel
                {
                    Icon = Icon,
                    Account = Account,
                    Password = Password,
                    Name = Title,
                    Description = Description,
                    CategoryKey = CategoryKey
                };
                int result = 0;
                if (DataItem != null)
                {
                    item.Id = DataItem.Id;
                    result = secureDatabase.SecureUpdate<DataItemModel>(item, secureKeyService.SecureKey);
                }
                else
                {
                    result = secureDatabase.SecureInsert<DataItemModel>(item, secureKeyService.SecureKey);
                }
                tcs.SetResult(result == 1?item:null);
            });
            return tcs.Task;
        }

        private void UpdateFontIcon(FontIcon data)
        {
            FontIconSource = data.Icon;
            FontIconBg = data.Background;
        }

        private async Task GenerateExcuteAsync()
        {
            await NavigationService.PushAsync(new PwdGeneratePage((pwd) => 
            {
                Password = pwd;
            }));
        }

        private async Task ImageTapExcuteAsync()
        {
            try
            {
                await NavigationService.PushPopupPageAsync(new IconSelectPage((data) =>
                {
                    Icon = data.ToJson();
                    UpdateFontIcon(data);
                }));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
