Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Ribbon

Namespace DevExpress.DevAV.Views
    Partial Public Class DevAVDbView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
    Public Class ObjectsEqualityConverter
        Implements System.Windows.Data.IValueConverter

        Public Property Inverse() As Boolean
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then
                Return value
            End If
            Dim result = String.Equals(value.ToString(), parameter)
            Return If(Inverse, (Not result), result)
        End Function
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Return If(DirectCast(value, Boolean), parameter, Nothing)
        End Function
    End Class
    Public Class RibbonStyleSelectorItem
        Inherits BarSplitButtonItem


        Private Shared globalRibbonStyle_Renamed As RibbonStyle = DevExpress.Xpf.Ribbon.RibbonStyle.Office2010
        Public Shared Property GlobalRibbonStyle() As RibbonStyle
            Get
                Return globalRibbonStyle_Renamed
            End Get
            Set(ByVal value As RibbonStyle)
                If Object.Equals(value, globalRibbonStyle_Renamed) Then
                    Return
                End If
                globalRibbonStyle_Renamed = value
                RaiseEvent GlobalRibbonStyleChanged(Nothing, EventArgs.Empty)
            End Set
        End Property
        Public Shared Event GlobalRibbonStyleChanged As EventHandler(Of EventArgs)
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(RibbonStyleSelectorItem), New FrameworkPropertyMetadata(GetType(RibbonStyleSelectorItem)))
            AddHandler GlobalRibbonStyleChanged, Sub(s, e)
            End Sub
        End Sub
        Private popupContentControl As New ContentControl()
        Public Sub New()
            ActAsDropDown = True
            ItemClickBehaviour = PopupItemClickBehaviour.CloseAllPopups
            PopupControl = New PopupControlContainer() With {.Content = popupContentControl}
        End Sub
        Protected Overrides Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MyBase.OnLoaded(sender, e)
            SelectedRibbonStyle = GlobalRibbonStyle
            AddHandler GlobalRibbonStyleChanged, AddressOf RibbonStyleSelectorItem_GlobalRibbonStyleChanged
        End Sub
        Protected Overrides Sub OnUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            MyBase.OnUnloaded(sender, e)
            RemoveHandler GlobalRibbonStyleChanged, AddressOf RibbonStyleSelectorItem_GlobalRibbonStyleChanged
        End Sub
        Private Sub RibbonStyleSelectorItem_GlobalRibbonStyleChanged(ByVal sender As Object, ByVal e As EventArgs)
            SelectedRibbonStyle = GlobalRibbonStyle
        End Sub
        Public Property SelectedRibbonStyle() As RibbonStyle
            Get
                Return CType(GetValue(SelectedRibbonStyleProperty), RibbonStyle)
            End Get
            Set(ByVal value As RibbonStyle)
                SetValue(SelectedRibbonStyleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly SelectedRibbonStyleProperty As DependencyProperty = DependencyProperty.Register("SelectedRibbonStyle", GetType(RibbonStyle), GetType(RibbonStyleSelectorItem), New PropertyMetadata(DevExpress.Xpf.Ribbon.RibbonStyle.Office2010, AddressOf OnSelectedRibbonStyleChanged))
        Private Shared Sub OnSelectedRibbonStyleChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            GlobalRibbonStyle = DirectCast(e.NewValue, RibbonStyle)
        End Sub
        Public Property PopupTemplate() As DataTemplate
            Get
                Return CType(GetValue(PopupTemplateProperty), DataTemplate)
            End Get
            Set(ByVal value As DataTemplate)
                SetValue(PopupTemplateProperty, value)
            End Set
        End Property
        Public Shared ReadOnly PopupTemplateProperty As DependencyProperty = DependencyProperty.Register("PopupTemplate", GetType(DataTemplate), GetType(RibbonStyleSelectorItem), New PropertyMetadata(Nothing, AddressOf OnPopupTemplatePropertyChanged))
        Private Shared Sub OnPopupTemplatePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, RibbonStyleSelectorItem).popupContentControl.ContentTemplate = DirectCast(e.NewValue, DataTemplate)
            CType(d, RibbonStyleSelectorItem).popupContentControl.Content = New With {Key .Selector = d}
        End Sub
    End Class
End Namespace
