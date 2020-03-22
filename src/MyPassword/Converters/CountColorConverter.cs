using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyPassword.Converters
{
    public class CountColorConverter : IValueConverter
    {
        object ValueColor = null;
        object NoValueColor = null;

        public CountColorConverter()
        {
            InitColor();
        }

        void InitColor()
        {
            if(ValueColor == null) ValueColor = GetColor("ValueColor");
            if(NoValueColor == null) NoValueColor = GetColor("NoValueColor");
        }

        static object GetColor(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out object color))
            {
                return color;
            }
            else
            {
                return Color.Red;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((int)value) == 0)
            {
                return NoValueColor;
            }
            else
            {
                return ValueColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
