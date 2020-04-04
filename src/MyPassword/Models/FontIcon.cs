using MyPassword.Const;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Models
{
    public class FontIcon
    {
        public string Icon { get; set; }

        public string Background { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static FontIcon ToFontIcon(string json)
        {
            if (string.IsNullOrEmpty(json)) return CreateDefaultFontIcon();
            try
            {
                var icon = JsonConvert.DeserializeObject<FontIcon>(json);
                return icon;
            }
            catch
            {
                return CreateDefaultFontIcon();
            }
        }

        public static FontIcon CreateDefaultFontIcon()
        {
            return new FontIcon
            {
                Icon = IconFont.DefaultAppIcon,
                Background = "#9F35FF",
            };
        }
    }
}
