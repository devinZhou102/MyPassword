using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Dependencys
{
    public interface IGetSQLitePlatformService
    {
        ISQLitePlatform GetSQLitePlatform();
    }
}
