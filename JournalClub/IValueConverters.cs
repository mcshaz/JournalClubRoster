using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace JournalClub
{
    public class IsTwoValuesEqualConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) 
        {
            if (targetType != typeof(object)) { throw new NotImplementedException(); }
            if (values.Length != 2) { throw new ArgumentException(); }
            return values[0].Equals(values[1]);
        }

        public object[] ConvertBack(object Value, Type[] Target_Type, object Parameter, CultureInfo culture)
        {
            throw new NotSupportedException ();
        }

    }
    public class GetBindingForDebug : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return parameter == null; }
            return value.Equals(parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
