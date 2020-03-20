using MyPassword.Dependencys;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.UWP.Dependencys.ImpUWPGetSQLitePlatformService))]

namespace MyPassword.UWP.Dependencys
{
    class ImpUWPGetSQLitePlatformService : IGetSQLitePlatformService
    {
        public ISQLitePlatform GetSQLitePlatform()
        {
            return new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        }
    }
}
