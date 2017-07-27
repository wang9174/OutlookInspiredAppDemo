Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Markup

Namespace DevExpress.DevAV
    Public Class SelectedItemsConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If
            Dim result = New List(Of Object)()
            For Each item In DirectCast(value, List(Of Employee))
                result.Add(item)
            Next item
            Return result
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim result As New List(Of Employee)()
            If value IsNot Nothing Then
                For Each item As Object In (DirectCast(value, List(Of Object)))
                    If TryCast(item, Employee) IsNot Nothing Then
                        result.Add(DirectCast(item, Employee))
                    End If
                Next item
            End If
            Return result
        End Function
    End Class
End Namespace
