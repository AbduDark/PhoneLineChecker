using System;
using System.Globalization;
using System.Windows.Data;

namespace PhoneLinesApp.UI.Converters
{
    public class ActivationWarningConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is DateTime lastDate && values[1] is bool isActive)
            {
                int days = 7;
                if (parameter != null && int.TryParse(parameter.ToString(), out var p))
                    days = p;

                return isActive && (DateTime.Now - lastDate).TotalDays > days;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
