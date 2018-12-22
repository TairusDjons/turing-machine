using System;
using System.Globalization;
using System.Windows.Data;

namespace TuringMachine.IDE.WPF.Converters
{
    [ValueConversion(typeof(char?), typeof(string))]
    public class OptionalCharToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case char symbol:
                    return new string(symbol, 1);
                case null:
                    return null;
                default:
                    throw new ArgumentException((string)value, nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case string text when string.IsNullOrEmpty(text):
                {
                    return null;
                }
                case string text when text.Length == 1:
                {
                    return text[0];
                }
                default:
                {
                    throw new ArgumentException((string)value, nameof(value));
                }
            }
        }
    }
}
