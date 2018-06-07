using SQLite.Net.Cipher.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.DataBase
{
    public interface IPasswordDataBase: ISecureDatabase
    {
        void DeleteTables();
        void DeleteTable(Type type);
    }
}
