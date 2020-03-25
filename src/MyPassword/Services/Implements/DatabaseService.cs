using MyPassword.Models;
using SQLite.Net.Cipher.Data;
using SQLite.Net.Cipher.Interfaces;
using SQLite.Net.Cipher.Security;
using SQLite.Net.Interop;
using System;

namespace MyPassword.Services
{
    public partial class DatabaseService : SecureDatabase, IDataBaseService
    {


        public DatabaseService(ISQLitePlatform platform, string dbfile, CryptoService salt) : base(platform, dbfile, salt)
        {
        }


        public void CreateTable(Type type)
        {
            base.CreateTable(type);
        }

        public void DeleteTable(Type type)
        {
            DropTable(type);
        }

        public void DeleteTables()
        {
            DropTable<DataItemModel>();
        }

        protected override void CreateTables()
        {
            CreateTable<DataItemModel>();
        }

    }
}
