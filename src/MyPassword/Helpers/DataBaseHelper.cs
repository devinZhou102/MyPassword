using MyPassword.DataBase;
using MyPassword.IServices;
using MyPassword.Models;
using Plugin.NetStandardStorage;
using Plugin.NetStandardStorage.Abstractions.Types;
using Plugin.NetStandardStorage.Implementations;
using SQLite.Net.Cipher.Interfaces;
using SQLite.Net.Cipher.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MyPassword.Helpers
{
    public class DataBaseHelper
    {
        public ISecureDatabase Database { get; private set; }

        private string SecureKey;


        public const string DataBaseFolder = "Database";
        public const string DBName = "MyPassword.db";

        private static Lazy<DataBaseHelper> instance = new Lazy<DataBaseHelper>(() => new DataBaseHelper());

        public static DataBaseHelper Instance => instance.Value;

        private DataBaseHelper()
        {
            CreateDataBaseFolder();
        }

        private void CreateDataBaseFolder()
        {
            try
            {
                CrossStorage.FileSystem.LocalStorage.CreateFolder(DataBaseFolder, CreationCollisionOption.FailIfExists);

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void ConnectDataBase(string secureKey)
        {
            SecureKey = secureKey;
            CryptoService service = new CryptoService(SecureKey);
            try
            {
                var platformService = DependencyService.Get<IGetSQLitePlatformService>();
                if(platformService != null)
                {
                    Database = new PwdDataBase(platformService.GetSQLitePlatform(), GetDBPath(), service);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(" Exception  ==== " + e.StackTrace);
            }
        }

        private string GetDBPath()
        {
            string path = Path.Combine(CrossStorage.FileSystem.LocalStorage.FullPath, DataBaseFolder + "/" + DBName);
            return path;
        }

        
    }
}
