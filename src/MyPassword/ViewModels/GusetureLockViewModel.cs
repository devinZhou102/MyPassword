using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyPassword.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public class GusetureLockViewModel:BaseGuestureLockViewModel
    {

        private int Count;

        private string CacheLock = "";

        Action ActionSetLockFinish;

        public GusetureLockViewModel(Action actionSetLockFinish)
        {
            ActionSetLockFinish = actionSetLockFinish;
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
                    LockManager.Instance.Save(strLock);
                    ActionSetLockFinish?.Invoke();
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
