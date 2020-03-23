using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Const;
using MyPassword.Helpers;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordViewModel : BaseViewModel
    {

        private ObservableCollection<PwdItemViewModel> _PasswordList;

        public ObservableCollection<PwdItemViewModel> PasswordList
        {
            get
            {
                if (null == _PasswordList)
                {
                    _PasswordList = new ObservableCollection<PwdItemViewModel>();
                }
                return _PasswordList;
            }
            set
            {
                _PasswordList = value;
                RaisePropertyChanged(nameof(PasswordList));
            }
        }

        private bool _CategoryVisible;
        public bool CategoryVisible
        {
            get => _CategoryVisible;
            set
            {
                _CategoryVisible = value;
                RaisePropertyChanged(nameof(CategoryVisible));
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

        private ISecureKeyService secureKeyService;
        private ICategoryService categoryService;
        public PasswordViewModel(ISecureKeyService secureKeyService, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.secureKeyService = secureKeyService;
            RegisterMessager();
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            CategoryKey = parameter as string;
            CategoryVisible = false;
            if (!string.IsNullOrEmpty(CategoryKey))
            {
                var c = categoryService.FindCategoryByKey(CategoryKey);
                if (c != null)
                {
                    CategoryVisible = true;
                    UpdateCategory(c);
                }
            }
            LoadData();
            return base.InitializeAsync(parameter);
        }


        private void UpdateCategory(CategoryModel category)
        {
            CategoryName = category.Name;
            CategoryIcon = category.Icon;
            CategoryKey = category.Key;
            CategoryBackground = category.Background;
        }

        public void LoadData()
        {
            var datas = DataBaseHelper.Instance.Database?.SecureGetAll<DataItemModel>(secureKeyService.SecureKey);
            PasswordList.Clear();
            if (null != datas)
            {
                foreach (var item in datas)
                {
                    PasswordList.Add(Trans2PwdItemViewModel(item));
                }
            }
        }


        private void RegisterMessager()
        {
            MessengerInstance.Register<DataItemModel>(this, TokenConst.TokenUpdate, (data) =>
              {
                  if (data != null)
                  {
                      var pwdvm = Trans2PwdItemViewModel(data);
                      var item = PasswordList.Where((v) => v.Id == data.Id);
                      if (item != null && item.Count() > 0)
                      {
                          int index = PasswordList.IndexOf(item.First());
                          PasswordList.RemoveAt(index);

                          PasswordList.Insert(index, pwdvm);
                      }
                      else
                      {
                          PasswordList.Add(pwdvm);
                      }
                  }
              });

            MessengerInstance.Register<int>(this, (value) =>
             {
                 if (value == TokenConst.TokenUpdateList)
                 {
                     LoadData();
                 }
             });

            MessengerInstance.Register<DataItemModel>(this, TokenConst.TokenDelete, (value) =>
            {
                var item = PasswordList.Where((v) => v.Id == value.Id);
                if (item != null && item.Count() > 0)
                {
                    int index = PasswordList.IndexOf(item.First());
                    PasswordList.RemoveAt(index);
                }
            });
        }

        private DataItemModel Trans2DataItemModel(PwdItemViewModel item)
        {
            return new DataItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Account = item.Account,
                Icon = item.Icon,
                CategoryKey = item.CategoryKey,
                Password = item.Password,
                Description = item.Description,
            };
        }

        private PwdItemViewModel Trans2PwdItemViewModel(DataItemModel item)
        {
            return new PwdItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Account = item.Account,
                Icon = item.Icon,
                CategoryKey = item.CategoryKey,
                Password = item.Password,
                Description = item.Description,
                TappedCommand = TappedCommand
            };
        }

        public ICommand AddDataCommand => new RelayCommand(async () =>
            await NavigationService.PushAsync(new PasswordEditPage()));

        public ICommand TappedCommand => new RelayCommand<PwdItemViewModel>(async (item) =>
            await NavigationService.PushAsync(new PasswordEditPage(Trans2DataItemModel(item))));

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

    }

    public class PwdItemViewModel : BaseViewModel
    {

        public int Id { get; set; }

        public string Icon { get; set; }

        public string CategoryKey { get; set; }

        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public ICommand TappedCommand { get; set; }


    }

}
