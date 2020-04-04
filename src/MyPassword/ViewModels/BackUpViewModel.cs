using GalaSoft.MvvmLight.Command;
using MyPassword.Const;
using MyPassword.Localization;
using MyPassword.Models;
using MyPassword.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public class BackUpViewModel:BaseViewModel
    {

        private string _Data;
        public string Data
        {
            get => _Data ?? (_Data = "");
            set
            {
                _Data = value;
                RaisePropertyChanged(nameof(Data));
            }
        }

        private readonly ISecureKeyService secureKeyService;
        private readonly IDataBaseService dataBaseService;
        private readonly ICategoryService categoryService;

        private string DefaultCategoryKey = "";

        public BackUpViewModel(ISecureKeyService secureKeyService,IDataBaseService dataBaseService,ICategoryService categoryService)
        {
            this.secureKeyService = secureKeyService;
            this.dataBaseService = dataBaseService;
            this.categoryService = categoryService;
        }


        public override Task InitializeAsync<T>(T parameter)
        {
            DefaultCategoryKey = categoryService.GetDefaultCategory().Key;
            Data = "";
            return base.InitializeAsync(parameter);
        }

        private void ExportDataExcute()
        {
            var datas = GetAllDatas();
            if (datas == null || datas.Count == 0)
            {
                Alert(AppResource.ExportNoDatas);
            }
            else
            {
                Data = JsonConvert.SerializeObject(datas);
            }
        }

        private List<DataItemModel> GetAllDatas()
        {
            string query = $"Select * from {DataItemModel.TableName}";
            var datas = dataBaseService.SecureQuery<DataItemModel>(query, secureKeyService.SecureKey);
            return datas;
        }

        private async Task DeleteAllDatas()
        {
            await Task.Factory.StartNew(() => {
                string query = $"Delete from {DataItemModel.TableName}";
                dataBaseService.SecureQuery<DataItemModel>(query, secureKeyService.SecureKey);
            });
        }

        private async Task InsertAllDatasAsync(List<DataItemModel> datas)
        {
            await Task.Factory.StartNew(() => {

                if (datas != null)
                {
                    foreach (var item in datas)
                    {
                        dataBaseService.SecureInsert<DataItemModel>(item, secureKeyService.SecureKey);
                    }
                }
            });
        }


        void Alert(string msg)
        {
            alertService.DisplayAlert("", msg, AppResource.DialogButtonConfirm);
        }

        private async Task ImportDataExcuteAsync()
        {
            try
            {
                var list = JsonConvert.DeserializeObject<List<DataItemModel>>(Data.Trim());
                if(list?.Count > 0)
                {
                    bool valid = IsDatasValid(list);
                    if (valid)
                    {
                        loadingService.ShowLoading();
                        await DeleteAllDatas();
                        await InsertAllDatasAsync(list);
                        loadingService.HideLoading();
                        MessengerInstance.Send<int>(TokenConst.TokenUpdateList);
                        Alert(AppResource.ImportSuccess);
                    }
                    else
                    {
                        Alert(AppResource.ImportErrorData);
                    }
                }
                else
                {
                    Alert(AppResource.ImportErrorData);
                }

            }
            catch
            {
                Alert(AppResource.ImportErrorData);
            }
            finally
            {
                loadingService.HideLoading();
            }
        }

        private bool IsDatasValid(List<DataItemModel> datas)
        {

            bool IsDataValid(DataItemModel dataItem)
            {
                if (dataItem == null) return false;
                if (string.IsNullOrEmpty(dataItem.Name)) return false;
                if (string.IsNullOrEmpty(dataItem.Password)) return false;
                if (string.IsNullOrEmpty(dataItem.Account)) return false;
                if (categoryService.FindCategoryByKey(dataItem.CategoryKey) == null)
                {
                    dataItem.CategoryKey = DefaultCategoryKey;
                }
                var fi = GetFontIcon(dataItem.Icon);
                dataItem.Icon = fi.ToJson();
                return true;
            }

            foreach (var item in datas)
            {
                if (!IsDataValid(item))
                {
                    return false;
                }
            }
            return true;

        }


        private FontIcon GetFontIcon(string icon)
        {
            try
            {
                var fi = JsonConvert.DeserializeObject<FontIcon>(icon);
                Color.FromHex(fi.Background);
                return fi;
            }
            catch
            {
                return FontIcon.CreateDefaultFontIcon();
            }
        }

        public ICommand ExportDataCommand => new RelayCommand(ExportDataExcute);
        public ICommand ImportDataCommand => new RelayCommand(async ()=> await ImportDataExcuteAsync());

        public ICommand CopyDataCommand => new RelayCommand(async ()=>
        {
            await Clipboard.SetTextAsync(Data);
            alertService.Toast(AppResource.CopySuccess);
        });

    }
}
