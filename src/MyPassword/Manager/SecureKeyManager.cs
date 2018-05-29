using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;

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
            Read();
        }

        private void Read()
        {
            SecureKey = CrossSecureStorage.Current.GetValue(KEY_SECURE_KEY, "");
        }

        public bool Save(string value)
        {
            if(CrossSecureStorage.Current.SetValue(KEY_SECURE_KEY, value))
            {
                SecureKey = value;
                return true;
            }
           return false;
        }

    }
}
