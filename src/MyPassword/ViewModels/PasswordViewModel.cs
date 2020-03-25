using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyPassword.Const;
using MyPassword.Models;
using MyPassword.Pages;
using MyPassword.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        private ISecureKeyService secureKeyService;
        private ICategoryService categoryService;
        private IDataBaseService dataBaseSerice;

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

        private void QueryDatas(string key)
        {
            List<DataItemModel> datas = null;
            string query;
            bool hasWhere;
            List<string> param = new List<string>();
            if (string.IsNullOrEmpty(CategoryKey))
            {
                query = "Select * from DataItemModel";
                hasWhere = false;
            }
            else
            {
                hasWhere = true;
                query = string.Format("Select * from DataItemModel where CategoryKey = '{0}'", CategoryKey);
                //query = "Select * from DataItemModel where CategoryKey = '{0}'";
                //param.Add(CategoryKey);
            }
            if (!string.IsNullOrEmpty(key))
            {
                byte[] bytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(key));
                string searchKey = Encoding.UTF8.GetString(bytes);
                string splitter = hasWhere?" and ":" where ";
                var q = $"Name like '%{searchKey}%'";
                query = string.Join(splitter, query, q);
                param.Add(key);
            }

            Debug.WriteLine("query : "+ query);
            try
            {
                dataBaseSerice.SecureQuery<DataItemModel>(query, secureKeyService.SecureKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            UpdateDatas(datas);
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
                     QueryDatas("");
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
                Description = item.Description,
                TappedCommand = TappedCommand
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

        public ICommand TappedCommand { get; set; }


    }

}
