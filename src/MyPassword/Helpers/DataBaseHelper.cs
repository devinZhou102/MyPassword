using MyPassword.DataBase;
using MyPassword.Dependencys;
using Plugin.NetStandardStorage;
using Plugin.NetStandardStorage.Abstractions.Types;
using SQLite.Net.Cipher.Security;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPassword.Helpers
{
    public class DataBaseHelper
    {
        public IPasswordDataBase Database { get; private set; }

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

        public Task<bool> ConnectDataBase(string secureKey)
        {
            SecureKey = secureKey;

            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(()=>
            {
                CryptoService service = new CryptoService(SecureKey);
                try
                {
                    var platformService = DependencyService.Get<IGetSQLitePlatformService>();
                    if (platformService != null)
                    {
                        Database = new PwdDataBase(platformService.GetSQLitePlatform(), GetDBPath(), service);
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(" Exception  ==== " + e.StackTrace);
                }

                if (Database != null)
                {
                    tcs.SetResult(true);
                }
                else
                {
                    tcs.SetResult(false);
                }
            });
            return tcs.Task;
        }

        private string GetDBPath()
        {
            string path = Path.Combine(CrossStorage.FileSystem.LocalStorage.FullPath, DataBaseFolder + "/" + DBName);
            return path;
        }

        
    }
}
