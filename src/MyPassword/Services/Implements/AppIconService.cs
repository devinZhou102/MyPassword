using MyPassword.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public class AppIconService : IAppIconService
    {
        private const string ResourcePrefix = "MyPassword.EmbeddedResource";
        private const string ResourceName = "appdatas.json";

        public List<FontIcon> FontIcons { get; private set; }

        public AppIconService()
        {

        }

        public Task<bool> LoadAssets()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppIconService)).Assembly;
            var res = $"{ResourcePrefix}.{ResourceName}";
            using (var stream = assembly.GetManifestResourceStream(res))
            {
                string json = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    try
                    {
                        json = reader.ReadToEnd();
                        FontIcons = JsonConvert.DeserializeObject<List<FontIcon>>(json);
                    }
                    catch
                    {
                        return Task.FromResult(false);
                    }
                }
            }
            return Task.FromResult(true);
        }

    }
}
