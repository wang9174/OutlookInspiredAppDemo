Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.Common.View
    Public Class ZoomToFitMapBehavior
        Inherits Behavior(Of MapControl)

        Public Sub New()
            mapVectorLayers = New List(Of VectorLayer)()
        End Sub
        Private mapVectorLayers As List(Of VectorLayer)

        Public Shared ReadOnly PaddingFactorProperty As DependencyProperty = DependencyProperty.Register("PaddingFactor", GetType(Double), GetType(ZoomToFitMapBehavior), New FrameworkPropertyMetadata(0.15R, Sub(d, e) CType(d, ZoomToFitMapBehavior).OnPaddingFactorChanged(e)))
        Public Property PaddingFactor() As Double
            Get
                Return CDbl(GetValue(PaddingFactorProperty))
            End Get
            Set(ByVal value As Double)
                SetValue(PaddingFactorProperty, value)
            End Set
        End Property
        Public Property ZoomLayerName() As String

        Private Sub OnPaddingFactorChanged(ByVal e As DependencyPropertyChangedEventArgs)
            ZoomToFit()
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler Me.AssociatedObject.Loaded, AddressOf MapControlLoaded
            AddHandler Me.AssociatedObject.Unloaded, AddressOf MapControlUnLoaded
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler Me.AssociatedObject.Loaded, AddressOf MapControlLoaded
            RemoveHandler Me.AssociatedObject.Unloaded, AddressOf MapControlUnLoaded
            MyBase.OnDetaching()
        End Sub

        Private Sub MapControlLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            mapVectorLayers.Clear()
            For Each layer In Me.AssociatedObject.Layers
                Dim vectorLayer As VectorLayer = TryCast(layer, VectorLayer)
                If vectorLayer IsNot Nothing AndAlso vectorLayer.Data IsNot Nothing AndAlso (If(String.IsNullOrEmpty(ZoomLayerName), True, vectorLayer.Name = ZoomLayerName)) Then
                    mapVectorLayers.Add(vectorLayer)
                    RemoveHandler vectorLayer.Loaded, AddressOf DisplayItemsCollectionChanged
                    AddHandler vectorLayer.Loaded, AddressOf DisplayItemsCollectionChanged
                End If
            Next layer
        End Sub

        Private Sub MapControlUnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each layer As VectorLayer In mapVectorLayers
                RemoveHandler layer.Loaded, AddressOf DisplayItemsCollectionChanged
            Next layer
        End Sub

        Private Sub DisplayItemsCollectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ZoomToFit()
        End Sub
        Private Sub ZoomToFit()
            If Me.AssociatedObject IsNot Nothing AndAlso mapVectorLayers.Capacity > 0 Then
                Me.AssociatedObject.ZoomToFitLayerItems(mapVectorLayers, PaddingFactor)
            End If
        End Sub
    End Class
End Namespace
