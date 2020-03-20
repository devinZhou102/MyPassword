using MyPassword.Dependencys;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.iOS.Dependencys.ImpiOSGetSQLitePlatformService))]

namespace MyPassword.iOS.Dependencys
{
    class ImpiOSGetSQLitePlatformService : IGetSQLitePlatformService
    {
        public ISQLitePlatform GetSQLitePlatform()
        {
            return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
        }
    }
}
