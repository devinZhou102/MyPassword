using SQLite.Net.Cipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services
{
    public interface IDataBaseService : ISecureDatabase
    {
        void DeleteTables();
        void DeleteTable(Type type);

        void CreateTable(Type type);
    }
}
