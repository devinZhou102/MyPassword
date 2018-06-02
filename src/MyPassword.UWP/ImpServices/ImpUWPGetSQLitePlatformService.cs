using MyPassword.IServices;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.UWP.ImpServices.ImpUWPGetSQLitePlatformService))]

namespace MyPassword.UWP.ImpServices
{
    class ImpUWPGetSQLitePlatformService : IGetSQLitePlatformService
    {
        public ISQLitePlatform GetSQLitePlatform()
        {
            return new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        }
    }
}
