using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyPassword.Services
{
    public class SecureKeyService : ISecureKeyService
    {
        private const string KEY_SECURE_KEY = "secureKey";


        public string SecureKey { get; private set; }

        public SecureKeyService()
        {
        }


        public async Task LoadSecureKeyAsync()
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
