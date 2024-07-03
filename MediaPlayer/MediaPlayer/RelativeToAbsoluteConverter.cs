using System;
using System.Globalization;
using System.Windows.Data;

namespace MediaPlayer
{
    internal class RelativeToAbsoluteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            string relative = (string)value;

            if (relative.Contains(':'))
            {
                result = relative;
            }
            else
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                result = $"{folder}{relative}";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}