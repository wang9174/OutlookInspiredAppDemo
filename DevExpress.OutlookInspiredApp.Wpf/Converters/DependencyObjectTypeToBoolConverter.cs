using System;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.DevAV {
    public class DependencyObjectTypeToBoolConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            DependencyObject dependencyObject = (DependencyObject)value;
            return ((dependencyObject == null) ? string.Empty : dependencyObject.GetType().FullName).Contains(parameter.ToString());
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
