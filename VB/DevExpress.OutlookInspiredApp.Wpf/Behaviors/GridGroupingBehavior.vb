Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV
    Public Class GridGroupingBehavior
        Inherits Behavior(Of GridControl)

        Private tableView As TableView

        Public Shared ReadOnly GroupProperty As DependencyProperty = DependencyProperty.Register("Group", GetType(String), GetType(GridGroupingBehavior), New FrameworkPropertyMetadata(Nothing, Sub(d, e) CType(d, GridGroupingBehavior).OnGroupChanged(e)))
        Public Property Group() As String
            Get
                Return CStr(GetValue(GroupProperty))
            End Get
            Set(ByVal value As String)
                SetValue(GroupProperty, value)
            End Set
        End Property
        Public Shared ReadOnly NewItemCommandProperty As DependencyProperty = DependencyProperty.Register("NewItemCommand", GetType(ICommand), GetType(GridGroupingBehavior), New PropertyMetadata(Nothing))
        Public Property NewItemCommand() As ICommand
            Get
                Return DirectCast(GetValue(NewItemCommandProperty), ICommand)
            End Get
            Set(ByVal value As ICommand)
                SetValue(NewItemCommandProperty, value)
            End Set
        End Property
        Private Sub OnGroupChanged(ByVal e As DependencyPropertyChangedEventArgs)
            If AssociatedObject Is Nothing Then
                Return
            End If
            If String.IsNullOrEmpty(Group) Then
                AssociatedObject.ClearGrouping()
            End If
            If AssociatedObject.Columns.GetColumnByFieldName(Group) IsNot Nothing Then
                AssociatedObject.GroupBy(Group)
            End If
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Loaded, AddressOf Loaded
        End Sub
        Private Sub Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            tableView = TryCast(AssociatedObject.View, TableView)
            If tableView IsNot Nothing Then
                RemoveHandler tableView.FocusedRowHandleChanged, AddressOf FocusedRowHandleChanged
                AddHandler tableView.FocusedRowHandleChanged, AddressOf FocusedRowHandleChanged
            End If
        End Sub
        Private Sub FocusedRowHandleChanged(ByVal sender As Object, ByVal e As FocusedRowHandleChangedEventArgs)
            If e.RowData.RowHandle.Value = GridControl.NewItemRowHandle AndAlso AssociatedObject.SelectedItems.Count = 0 Then
                If NewItemCommand IsNot Nothing Then
                    NewItemCommand.Execute(Nothing)
                End If
                tableView.FocusedRowHandle = -1
            End If
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf Loaded
            If tableView IsNot Nothing Then
                RemoveHandler tableView.FocusedRowHandleChanged, AddressOf FocusedRowHandleChanged
            End If
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
