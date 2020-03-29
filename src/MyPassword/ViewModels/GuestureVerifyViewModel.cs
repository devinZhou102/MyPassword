using MyPassword.Localization;
using MyPassword.Services;
using System;
using System.Threading.Tasks;

namespace MyPassword.ViewModels
{
    public class GuestureVerifyViewModel:BaseGuestureLockViewModel
    {

        Action VerifySuccess;
        public GuestureVerifyViewModel(IGuestureLockService guestureLockService):base(guestureLockService)
        {
        }

        public override Task InitializeAsync<T>(T parameter)
        {
            VerifySuccess = parameter as Action;
            return base.InitializeAsync(parameter);
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
                Message = AppResource.MsgWrongGuesture;
                MessageColor = ColorRed;
            }
            return Task.CompletedTask;
        }


    }
}
