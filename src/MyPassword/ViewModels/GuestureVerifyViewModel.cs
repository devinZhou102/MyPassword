using MyPassword.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static MyPassword.Pages.GuestureVerifyPage;

namespace MyPassword.ViewModels
{
    public class GuestureVerifyViewModel:BaseGuestureLockViewModel
    {

        public Action VerifySuccess { get; set; }
        public GuestureVerifyViewModel()
        {
        }

        protected override void CreateGuestureLockSuccess(string strLock)
        {
            var cachelock = LockManager.Instance.GuestureLock;
            if(cachelock.Equals(strLock))
            {
                VerifySuccess?.Invoke();
            }
            else
            {
                Message = "手势密码错误";
                MessageColor = ColorRed;
            }
        }


    }
}
