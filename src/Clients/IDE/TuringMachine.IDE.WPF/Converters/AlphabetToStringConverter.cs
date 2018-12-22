using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace TuringMachine.IDE.WPF.Converters
{
    [ValueConversion(typeof(char[]), typeof(string))]
    public class AlphabetToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (!(value is IEnumerable<char> alphabet)) { return null; }
            foreach (var item in alphabet)
            {
                result += item;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string)?.ToCharArray();
        }
    }
}
