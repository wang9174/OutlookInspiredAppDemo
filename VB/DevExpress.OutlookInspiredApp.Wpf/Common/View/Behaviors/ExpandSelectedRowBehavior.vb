Imports System
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV.Common.View
    Public Class ExpandSelectedRowBehavior
        Inherits Behavior(Of GridControl)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoaded
            If AssociatedObject.IsLoaded Then
                ExpandSelectedRow()
            End If
        End Sub
        Private Sub OnAssociatedObjectLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ExpandSelectedRow()
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoaded
        End Sub
        Private Sub ExpandSelectedRow()
            Dim selectedRowHandles() As Integer = AssociatedObject.GetSelectedRowHandles()
            If selectedRowHandles IsNot Nothing AndAlso selectedRowHandles.Length > 0 Then
                Dim handle As Integer = selectedRowHandles.First()
                AssociatedObject.ExpandMasterRow(handle)
            End If
        End Sub
    End Class
End Namespace
