using MyPassword.DataBase;
using MyPassword.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services
{
    public class DatabaseService : IDatabaseService
    {

        private IPasswordDataBase Database => DataBaseHelper.Instance.Database;

        public DatabaseService()
        {
        }


        int IDatabaseService.SecureDelete<T>(string id)
        {
            return Database.SecureDelete<T>(id);
        }

        T IDatabaseService.SecureGet<T>(string id, string keySeed)
        {
            return Database.SecureGet<T>(id,keySeed);
        }

        List<T> IDatabaseService.SecureGetAll<T>(string keySeed)
        {
            return Database.SecureGetAll<T>(keySeed);
        }

        int IDatabaseService.SecureGetCount<T>()
        {
            return Database.SecureGetCount<T>();
        }

        int IDatabaseService.SecureInsert<T>(T obj, string keySeed)
        {
            return Database.SecureInsert(obj, keySeed);
        }

        int IDatabaseService.SecureInsertOrReplace<T>(T obj, string keySeed)
        {
           return Database.SecureInsertOrReplace(obj, keySeed);
        }

        int IDatabaseService.SecureUpdate<T>(T obj, string keySeed)
        {
            return Database.SecureUpdate(obj,keySeed);
        }
    }
}
