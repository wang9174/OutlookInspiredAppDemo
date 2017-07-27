Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data
Imports DevExpress.DevAV.Controls
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity

Namespace DevExpress.DevAV
    Public Class AdvancedDisplayTextBehavior
        Inherits Behavior(Of AdvancedNavBarGroup)

        Public Shared ReadOnly FiltersProperty As DependencyProperty = DependencyProperty.Register("Filters", GetType(ObservableCollection(Of FilterItem)), GetType(AdvancedDisplayTextBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, AdvancedDisplayTextBehavior).OnFiltersChanged(e)))
        Public Property Filters() As ObservableCollection(Of FilterItem)
            Get
                Return CType(GetValue(FiltersProperty), ObservableCollection(Of FilterItem))
            End Get
            Set(ByVal value As ObservableCollection(Of FilterItem))
                SetValue(FiltersProperty, value)
            End Set
        End Property
        Public Property DisplayProperties() As String()

        Private Sub OnFiltersChanged(ByVal e As DependencyPropertyChangedEventArgs)
            BindingOperations.ClearBinding(Me.AssociatedObject, AdvancedNavBarGroup.AdvancedDisplayTextProperty)
            For Each [property] As String In DisplayProperties
                Dim item As FilterItem = Filters.FirstOrDefault(Function(x) x.Name = [property])
                If item IsNot Nothing Then
                    BindingOperations.SetBinding(Me.AssociatedObject, AdvancedNavBarGroup.AdvancedDisplayTextProperty, New Binding("EntitiesCount") With {.Source = item, .Mode = BindingMode.OneWay})
                    Return
                End If
            Next [property]
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
        End Sub
        Protected Overrides Sub OnDetaching()
            BindingOperations.ClearBinding(Me.AssociatedObject, AdvancedNavBarGroup.AdvancedDisplayTextProperty)
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
