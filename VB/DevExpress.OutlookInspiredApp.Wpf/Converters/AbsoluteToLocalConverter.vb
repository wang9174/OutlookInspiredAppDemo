Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data

Namespace DevExpress.DevAV
    Public Class AbsoluteToLocalConverter
        Implements IValueConverter

        Public Property MaxValue() As Double
        Public Property MinValue() As Double

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Double) Then
                Return value
            End If
            Return DirectCast(value, Double) * (MaxValue - MinValue) + MinValue
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
