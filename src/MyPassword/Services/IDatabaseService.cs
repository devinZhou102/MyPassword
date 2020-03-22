using SQLite.Net.Cipher.Interfaces;
using System.Collections.Generic;

namespace MyPassword.Services
{
    public interface IDatabaseService
    {
        int SecureDelete<T>(string id) where T : class, IModel, new();

        T SecureGet<T>(string id, string keySeed) where T : class, IModel, new();
      
        List<T> SecureGetAll<T>(string keySeed) where T : class, IModel, new();
       
        int SecureGetCount<T>() where T : class, IModel, new();
      
        int SecureInsert<T>(T obj, string keySeed) where T : class, IModel, new();

        int SecureInsertOrReplace<T>(T obj, string keySeed) where T : class, IModel, new();
        
        int SecureUpdate<T>(T obj, string keySeed) where T : class, IModel, new();
    }
}
