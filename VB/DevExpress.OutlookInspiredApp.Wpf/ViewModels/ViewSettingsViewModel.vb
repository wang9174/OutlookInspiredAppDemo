Imports System
Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.DevAV
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class ViewSettingsViewModel
        Public Shared Function Create(ByVal viewKind As CollectionViewKind, ByVal parentViewModel As Object) As ViewSettingsViewModel
            Return ViewModelSource.Create(Function() New ViewSettingsViewModel(viewKind)).SetParentViewModel(parentViewModel)
        End Function

        Private defaultCollectionViewKind As CollectionViewKind

        Protected Sub New(ByVal defaultCollectionViewKind As CollectionViewKind)
            Me.defaultCollectionViewKind = defaultCollectionViewKind
            ResetView()
        End Sub
        Public Overridable Property ViewKind() As CollectionViewKind
        Public Overridable Property Orientation() As Orientation
        Public Overridable Property IsDataPaneVisible() As Boolean
        Public Overridable Property IsFilterPaneVisible() As Boolean
        Public Sub ResetView()
            ViewKind = defaultCollectionViewKind
            Orientation = Orientation.Horizontal
            IsDataPaneVisible = True
            IsFilterPaneVisible = True
        End Sub
        Public Function CanShowList() As Boolean
            Return Not Object.Equals(ViewKind, CollectionViewKind.ListView)
        End Function
        Public Function CanShowCard() As Boolean
            Return Not Object.Equals(ViewKind, CollectionViewKind.CardView)
        End Function
        Public Sub ShowList()
            ViewKind = CollectionViewKind.ListView
        End Sub
        Public Sub ShowCard()
            ViewKind = CollectionViewKind.CardView
        End Sub
        Public Sub DataPaneRight()
            Orientation = Orientation.Horizontal
            IsDataPaneVisible = True
        End Sub
        Public Function CanDataPaneRight() As Boolean
            Return Orientation <> Orientation.Horizontal OrElse Not IsDataPaneVisible
        End Function
        Public Sub DataPaneLeft()
            Orientation = Orientation.Vertical
            IsDataPaneVisible = True
        End Sub
        Public Function CanDataPaneLeft() As Boolean
            Return Orientation <> Orientation.Vertical OrElse Not IsDataPaneVisible
        End Function
        Public Sub DataPaneOff()
            IsDataPaneVisible = False
        End Sub
        Public Function CanDataPaneOff() As Boolean
            Return IsDataPaneVisible
        End Function
    End Class
End Namespace
