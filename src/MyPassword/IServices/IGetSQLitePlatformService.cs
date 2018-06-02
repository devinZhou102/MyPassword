using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.IServices
{
    public interface IGetSQLitePlatformService
    {
        ISQLitePlatform GetSQLitePlatform();
    }
}
