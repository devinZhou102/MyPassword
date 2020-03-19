using GalaSoft.MvvmLight.Command;
using MyPassword.Manager;
using System;
using System.Windows.Input;

namespace MyPassword.ViewModels
{
    public class GuestureLockViewModel:BaseGuestureLockViewModel
    {

        private int Count;

        private string CacheLock = "";

        public Action ActionSetLockFinish { get; set; }

        public GuestureLockViewModel()
        {
            ResetLockExcute();
            ResetLockCommand = new RelayCommand(()=>ResetLockExcute());
        }

        protected override void CreateGuestureLockSuccess(string strLock)
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
                    if(LockManager.Instance.Save(strLock))
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
