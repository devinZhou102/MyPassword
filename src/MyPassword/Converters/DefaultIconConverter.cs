using MyPassword.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyPassword.Converters
{
    public class DefaultIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || string.IsNullOrEmpty(value as string))
            {
                return IconHelper.GetIcon("IconLockBlueBg");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
