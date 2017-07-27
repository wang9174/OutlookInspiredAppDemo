Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data

Namespace DevExpress.DevAV
    Public Class HalfValueConverter
        Implements IValueConverter

        Public Property NegativeValue() As Boolean
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is Double) Then
                Return value
            End If
            Dim result = (DirectCast(value, Double)) / 2
            If NegativeValue Then
                result *= -1
            End If
            Return result
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
