using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NetUtilities.Domain.Converters
{
    public class TextTrimmingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                int lineBreakIndex = text.IndexOf(Environment.NewLine);
                if (lineBreakIndex != -1)
                {
                    text = text.Substring(0, lineBreakIndex);
                }

                if (text.Length > 70)
                {
                    text = text.Substring(0, 70) + "...";
                }

                return text;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
