using GalaSoft.MvvmLight.Command;
using MyPassword.Localization;
using MyPassword.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class GuestureLockViewModel:BaseGuestureLockViewModel
    {

        private int Count;

        private string CacheLock = "";

        Action ActionSetLockFinish;

        public GuestureLockViewModel(IGuestureLockService guestureLockService):base(guestureLockService)
        {
            ResetLockExcute();
            ResetLockCommand = new RelayCommand(()=>ResetLockExcute());
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            ActionSetLockFinish = parameter as Action;
            return base.InitializeAsync(parameter);
        }

        protected override async Task CreateGuestureLockSuccessAsync(string strLock)
        {
            if(1 == Count)
            {
                Count++;
                CacheLock = strLock;
                Message = AppResource.MsgGuestureAgain;
                MessageColor = ColorBlue;
            }
            else if(2 == Count)
            {
                if(CacheLock.Equals(strLock))
                {
                    Message = "";
                    var f = await guetureLockSerivce.SaveAsync(strLock);
                    if (f)
                    {
                        ActionSetLockFinish?.Invoke();
                    }
                    else
                    {
                        Message = AppResource.MsgGuestureSaveFailed;
                        MessageColor = ColorRed;
                    }
                }
                else
                {
                    Message = AppResource.MsgGuestureConfirmFailed;
                    MessageColor = ColorRed;
                }
            }
        }


        public ICommand ResetLockCommand { get; private set; }

        private void ResetLockExcute()
        {
            Count = 1;
            CacheLock = "";
            Message = AppResource.MsgSetGuesture;
            MessageColor = ColorBlue;
        }

    }
}
