using MyPassword.IServices;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.iOS.ImpServices.ImpiOSGetSQLitePlatformService))]

namespace MyPassword.iOS.ImpServices
{
    class ImpiOSGetSQLitePlatformService : IGetSQLitePlatformService
    {
        public ISQLitePlatform GetSQLitePlatform()
        {
            return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
        }
    }
}
