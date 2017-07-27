Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.DevAV

Namespace DevExpress.DevAV
    Public Class PictureConverter
        Implements IValueConverter

        Private Function IValueConverter_Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim picture As Picture = TryCast(value, Picture)
            Return If(picture Is Nothing, Nothing, picture.Data)
        End Function
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim data() As Byte = TryCast(value, Byte())
            Return If(data Is Nothing, Nothing, New Picture() With {.Data = data})
        End Function
    End Class
End Namespace
