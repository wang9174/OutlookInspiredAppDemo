Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.Views
    Partial Public Class NavigatorMapView
        Inherits UserControl

        Public Property DetailsForm() As FrameworkElement
            Get
                Return DirectCast(GetValue(DetailsFormProperty), FrameworkElement)
            End Get
            Set(ByVal value As FrameworkElement)
                SetValue(DetailsFormProperty, value)
            End Set
        End Property
        Public Shared ReadOnly DetailsFormProperty As DependencyProperty = DependencyProperty.Register("DetailsForm", GetType(FrameworkElement), GetType(NavigatorMapView), New PropertyMetadata(Nothing))

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub BingGeocodeDataProvider_LayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            For Each pushpinItem In args.Items
                Dim pushpin As MapPushpin = TryCast(pushpinItem, MapPushpin)
                If pushpin IsNot Nothing Then
                    DirectCast(DataContext, INavigatorMapViewModel).NewPushpinCreated(pushpin)
                End If
            Next pushpinItem
        End Sub
        Private Sub BingRouteDataProvider_LayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            CType(args.Items(1), MapPushpin).Text = "A"
            CType(args.Items(2), MapPushpin).Text = "B"
            mapControl.ZoomToRegion(CType(args.Items(1), MapPushpin).Location, CType(args.Items(2), MapPushpin).Location, 0.4)
        End Sub
    End Class
End Namespace
