using GalaSoft.MvvmLight.Command;
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
                Message = "再次绘制解锁图案";
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
                        Message = "保存解锁图案失败...";
                        MessageColor = ColorRed;
                    }
                }
                else
                {
                    Message = "与首次绘制不一致，请再次绘制";
                    MessageColor = ColorRed;
                }
            }
        }


        public ICommand ResetLockCommand { get; private set; }

        private void ResetLockExcute()
        {
            Count = 1;
            CacheLock = "";
            Message = "为了安全，请设置手势密码";
            MessageColor = ColorBlue;
        }

    }
}
