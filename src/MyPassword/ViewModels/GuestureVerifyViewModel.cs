using MyPassword.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using static MyPassword.Pages.GuestureVerifyPage;

namespace MyPassword.ViewModels
{
    public class GuestureVerifyViewModel:BaseGuestureLockViewModel
    {

        public delegate void GuestureVerifySuccessedEvent();

        readonly GuestureVerifySuccessedEvent SuccessedEvent;
        public GuestureVerifyViewModel(GuestureVerifySuccessedEvent successedEvent)
        {
            SuccessedEvent = successedEvent;
        }

        protected override void CreateGuestureLockSuccess(string strLock)
        {
            var cachelock = LockManager.Instance.GuestureLock;
            if(cachelock.Equals(strLock))
            {
                SuccessedEvent?.Invoke();
            }
        }
    }
}
