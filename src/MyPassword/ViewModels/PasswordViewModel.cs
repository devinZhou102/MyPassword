using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Const;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
            get => _CategoryBackground ?? (_CategoryBackground = "#667567");
            set
            {
                _CategoryBackground = value;
                RaisePropertyChanged(nameof(CategoryBackground));
            }
        }

        private string CategoryKey;

        private string _SearchKey;

        public string SearchKey
        {
            get => _SearchKey ?? (_SearchKey = "");
            set
            {
                _SearchKey = value;
                RaisePropertyChanged(nameof(SearchKey));
                if (string.IsNullOrEmpty(SearchKey)) QueryDatas("");
            }
        }

        private readonly ISecureKeyService secureKeyService;
        private readonly ICategoryService categoryService;
        private readonly IDataBaseService dataBaseSerice;


        private List<DataItemModel> PasswordCacheList;

        public PasswordViewModel(IDataBaseService dataBaseSerice,ISecureKeyService secureKeyService, ICategoryService categoryService)
        {
            this.dataBaseSerice = dataBaseSerice;
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
            LoadPasswordFromDataBase();
            QueryDatas("");
            return base.InitializeAsync(parameter);
        }


        private void UpdateCategory(CategoryModel category)
        {
            CategoryName = category.Name;
            CategoryIcon = category.Icon;
            CategoryKey = category.Key;
            CategoryBackground = category.Background;
        }

        private void UpdateDatas(List<DataItemModel> datas)
        {
            PasswordList.Clear();
            if (null != datas)
            {
                foreach (var item in datas)
                {
                    PasswordList.Add(Trans2PwdItemViewModel(item));
                }
            }
        }


        private void LoadPasswordFromDataBase()
        {
            string query = $"Select * from {DataItemModel.TableName}";
            if (!string.IsNullOrEmpty(CategoryKey))
            {
                query = $"{query} where CategoryKey = '{CategoryKey}'";
            }
            PasswordCacheList = dataBaseSerice.SecureQuery<DataItemModel>(query, secureKeyService.SecureKey);
        }

        private void QueryDatas(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                UpdateDatas(PasswordCacheList);
            }
            else
            {
                var datas = PasswordCacheList.FindAll((p) =>
                    p.Name?.Contains(key) == true ||
                    p.Description?.Contains(key) == true ||
                    p.Phone?.Contains(key) == true);
                UpdateDatas(datas);
            }
        }

        private void RegisterMessager()
        {
            MessengerInstance.Register<DataItemModel>(this, TokenConst.TokenUpdate, (data) =>
              {
                  Device.BeginInvokeOnMainThread(async ()=>
                  {
                      await Task.Delay(300);
                      if (data != null)
                      {
                          var item = PasswordCacheList.Where((v) => v.Id == data.Id);
                          if (item != null && item.Count() > 0)
                          {
                              int index = PasswordCacheList.IndexOf(item.First());
                              PasswordCacheList.RemoveAt(index);
                              PasswordCacheList.Insert(index, data);
                          }
                          else
                          {
                              PasswordCacheList.Add(data);
                          }
                          QueryDatas(SearchKey);
                      }
                  });
              });

            MessengerInstance.Register<int>(this, (value) =>
             {
                 if (value == TokenConst.TokenUpdateList)
                 {
                     Device.BeginInvokeOnMainThread(async () =>
                     {
                         await Task.Delay(300);
                         LoadPasswordFromDataBase();
                     });
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
                Phone = item.Phone,
                UpdateTime = item.UpdateTime,
                Website = item.Website,
                Password = item.Password,
                Description = item.Description,
            };
        }

        private PwdItemViewModel Trans2PwdItemViewModel(DataItemModel item)
        {
            var icon = FontIcon.ToFontIcon(item.Icon);
            return new PwdItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Account = item.Account,
                Icon = item.Icon,
                FontIconBackground = icon.Background,
                FontIconSource = icon.Icon,
                CategoryKey = item.CategoryKey,
                Password = item.Password,
                Phone = item.Phone,
                Website = item.Website,
                UpdateTime = item.UpdateTime,
                Description = item.Description,
                TappedCommand = TappedCommand,
                DeleteCommand = DeleteCommand
            };
        }

        public ICommand SearchCommand => new Command<string>((key)=>
        {
            QueryDatas(key);
        });

        public ICommand AddDataCommand => new RelayCommand(async () =>
            await NavigationService.PushAsync(new PasswordEditPage()));

        public ICommand TappedCommand => new RelayCommand<PwdItemViewModel>(async (item) =>
            await NavigationService.PushAsync(new PasswordEditPage(Trans2DataItemModel(item))));

        public ICommand DeleteCommand => new RelayCommand<PwdItemViewModel>((data) =>
        {
            var item = PasswordCacheList.Where((v) => v.Id == data.Id);
            if (item != null && item.Count() > 0)
            {
                int index = PasswordCacheList.IndexOf(item.First());
                PasswordCacheList.RemoveAt(index);
                var sql = $"delete from {DataItemModel.TableName} where id = {data.Id}";
                dataBaseSerice.SecureQuery<DataItemModel>(sql,secureKeyService.SecureKey);
                QueryDatas(SearchKey);
            }
        });

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

        public string FontIconSource { get; set; }
        public string FontIconBackground { get; set; }

        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }

        public string Website { get; set; }

        public DateTimeOffset UpdateTime { get; set; }

        public ICommand TappedCommand { get; set; }

        public ICommand DeleteCommand { get; set; }


    }

}
