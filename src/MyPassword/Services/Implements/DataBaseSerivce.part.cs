using MyPassword.Dependencys;
using Plugin.NetStandardStorage;
using Plugin.NetStandardStorage.Abstractions.Types;
using SQLite.Net.Cipher.Security;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace MyPassword.Services
{
    public partial class DataBaseSerivce
    {
        private const string DataBaseFolder = "Database";
        private const string DBName = "MyPassword.db";
        private const string SaltText = "mypassword";

        private static void CreateDataBaseFolder()
        {
            try
            {
                CrossStorage.FileSystem.LocalStorage.CreateFolder(DataBaseFolder, CreationCollisionOption.FailIfExists);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public static DatabaseService ConnectDataBase()
        {
            CreateDataBaseFolder();
            CryptoService service = new CryptoService(SaltText);
            try
            {
                
                var platformService = DependencyService.Get<IGetSQLitePlatformService>();
                if (platformService != null)
                {
                    return new DatabaseService(platformService.GetSQLitePlatform(), GetDBPath(), service);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(" Exception  ==== " + e.StackTrace);
            }
            return null;
        }


        private static string GetDBPath()
        {
            string path = Path.Combine(CrossStorage.FileSystem.LocalStorage.FullPath, DataBaseFolder + "/" + DBName);
            return path;
        }

    }
}
