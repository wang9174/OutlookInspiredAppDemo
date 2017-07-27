Imports System
Imports System.Collections.Generic
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.ViewModels
    Public Class CitiesMapViewModel
        Inherits MapViewModelBase


        Private linksViewModel_Renamed As LinksViewModel
        Public Shared Function Create() As CitiesMapViewModel
            Return ViewModelSource.Create(Function() New CitiesMapViewModel())
        End Function
        Protected Sub New()
            Cities = New List(Of CityViewModel)()
        End Sub
        Public Overridable Property Cities() As List(Of CityViewModel)
        Public Overridable Property Addresses() As HashSet(Of Address)
        Public ReadOnly Property LinksViewModel() As LinksViewModel
            Get
                If linksViewModel_Renamed Is Nothing Then
                    linksViewModel_Renamed = DevExpress.DevAV.ViewModels.LinksViewModel.Create()
                End If
                Return linksViewModel_Renamed
            End Get
        End Property
    End Class
End Namespace
