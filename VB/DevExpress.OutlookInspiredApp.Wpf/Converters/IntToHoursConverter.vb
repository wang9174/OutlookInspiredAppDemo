Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data

Namespace DevExpress.DevAV
    Public Class IntToHoursConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim intValue? As Decimal = CType(value, Decimal?)
            If intValue Is Nothing Then
                Return Nothing
            End If
            If intValue < 0 OrElse intValue > 23 Then
                Throw New ArgumentException("Incorrect value hours!", "value")
            End If
            Return If(intValue < 13, String.Format("{0}AM", intValue), String.Format("{0}PM", intValue - 12))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
