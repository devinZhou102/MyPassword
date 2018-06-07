using MyPassword.Models;
using SQLite.Net.Cipher.Data;
using SQLite.Net.Cipher.Security;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MyPassword.DataBase
{
    public class PwdDataBase : SecureDatabase,IPasswordDataBase
    {

        public PwdDataBase(ISQLitePlatform platform, string dbfile, CryptoService salt) : base(platform, dbfile, salt)
        {
        }

        protected override void CreateTables()
        {
            Debug.WriteLine("PwdDataBase === CreateTables");
            CreateTable<DataItemModel>();
        }


        public void DeleteTables()
        {
            DropTable(typeof(DataItemModel));
        }

        public void DeleteTable(Type type)
        {
            DropTable(type);
        }
    }
}
