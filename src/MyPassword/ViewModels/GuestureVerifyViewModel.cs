using MyPassword.Services;
using System;
using System.Threading.Tasks;

namespace MyPassword.ViewModels
{
    public class GuestureVerifyViewModel:BaseGuestureLockViewModel
    {

        public Action VerifySuccess { get; set; }
        public GuestureVerifyViewModel(IGuestureLockService guestureLockService):base(guestureLockService)
        {
        }

        protected override Task CreateGuestureLockSuccessAsync(string strLock)
        {
            var cachelock = guetureLockSerivce.GuestureLock;
            if(cachelock.Equals(strLock))
            {
                VerifySuccess?.Invoke();
            }
            else
            {
                Message = "手势密码错误";
                MessageColor = ColorRed;
            }
            return Task.CompletedTask;
        }


    }
}
