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
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class PasswordListViewModel:BaseViewModel
    {

        private ObservableCollection<DataItemModel> _PasswordList;

        public ObservableCollection<DataItemModel> PasswordList
        {
            get
            {
                if (null == _PasswordList)
                {
                    _PasswordList = new ObservableCollection<DataItemModel>();
                }
                return _PasswordList;
            }
            set
            {
                _PasswordList = value;
                RaisePropertyChanged(nameof(PasswordList));
            }
        }

        private bool _EmptyTipsVisible;
        public bool EmptyTipsVisible
        {
            get => _EmptyTipsVisible;
            set
            {
                _EmptyTipsVisible = value;
                RaisePropertyChanged(nameof(EmptyTipsVisible));
            }
        }

        private ISecureKeyService secureKeyService;
        public PasswordListViewModel(ISecureKeyService secureKeyService)
        {
            this.secureKeyService = secureKeyService;
            LoadData();
            RegisterMessager();
            AddDataCommand = new RelayCommand(() => AddDataExcute());
        }

        public void LoadData()
        {
            var datas = DataBaseHelper.Instance.Database?.SecureGetAll<DataItemModel>(secureKeyService.SecureKey);
            PasswordList.Clear();
            if (null != datas)
            {
                foreach (var item in datas)
                {
                    PasswordList.Add(item);
                }
            }
            UpdateEmptyTipsVisible();
        }

        private void UpdateEmptyTipsVisible()
        {
            EmptyTipsVisible = PasswordList == null || PasswordList.Count == 0;
        }

        private void RegisterMessager()
        {
            MessengerInstance.Register<DataItemModel>(this,TokenConst.TokenUpdate,(data) => 
            {
                if(data != null)
                {
                  var item = PasswordList.Where((v) => v.Id == data.Id);
                  if(item != null && item.Count() > 0)
                  {
                       int index = PasswordList.IndexOf(item.First());
                       PasswordList.RemoveAt(index);
                       PasswordList.Insert(index,data);
                  }
                  else
                  {
                      PasswordList.Add(data);
                  }
                }
                UpdateEmptyTipsVisible();
            });

            MessengerInstance.Register<int>(this,(value)=> 
            {
                if(value == TokenConst.TokenUpdateList)
                {
                    LoadData();
                }
            });

            MessengerInstance.Register<DataItemModel>(this, TokenConst.TokenDelete, (value)=> 
            {
                var item = PasswordList.Where((v) => v.Id == value.Id);
                if (item != null && item.Count() > 0)
                {
                    int index = PasswordList.IndexOf(item.First());
                    PasswordList.RemoveAt(index);
                }
                UpdateEmptyTipsVisible();
            });
        }
        
        public ICommand AddDataCommand { get; private set; }

        private void AddDataExcute()
        {
            NavigationService.PushAsync(new PasswordEditPage());
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        
    }
}
