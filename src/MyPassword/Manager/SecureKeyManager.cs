using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyPassword.Manager
{
    public class SecureKeyManager
    {

        private const string KEY_SECURE_KEY = "secureKey";

        private static Lazy<SecureKeyManager> instance = new Lazy<SecureKeyManager>(() => new SecureKeyManager());

        public static SecureKeyManager Instance => instance.Value;

        public string SecureKey { get; private set; }

        private SecureKeyManager()
        {
            ReadAsync();
        }

        private async void ReadAsync()
        {
            SecureKey = await SecureStorage.GetAsync(KEY_SECURE_KEY);
            if (SecureKey == null) SecureKey = "";
        }

        public async Task<bool> SaveAsync(string value)
        {
            await SecureStorage.SetAsync(KEY_SECURE_KEY, value);
            var result = await SecureStorage.GetAsync(KEY_SECURE_KEY);
            if (result != null && result.Equals(value))
            {
                SecureKey = value;
                return true;
            }
            return false;
        }

    }
}
