Imports System
Imports System.Windows
Imports System.Windows.Data

Namespace DevExpress.DevAV
    Public Class DependencyObjectTypeToBoolConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim dependencyObject As DependencyObject = DirectCast(value, DependencyObject)
            Return (If(dependencyObject Is Nothing, String.Empty, dependencyObject.GetType().FullName)).Contains(parameter.ToString())
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
