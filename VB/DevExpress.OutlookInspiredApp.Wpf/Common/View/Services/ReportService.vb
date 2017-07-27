Imports System
Imports System.ComponentModel
Imports System.Printing
Imports System.Windows
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Common.View
    Public MustInherit Class ReportServiceBase
        Inherits ServiceBase
        Implements IReportService

        Private Class PrintDirectXtraReportPreviewModel
            Inherits XtraReportPreviewModel

            Protected Overrides Sub PrintDirect(ByVal parameter As Object)
                If TypeOf parameter Is String Then
                    PrintDirect(DirectCast(parameter, String))
                ElseIf TypeOf parameter Is PrintQueue Then
                    PrintDirect(DirectCast(parameter, PrintQueue))
                Else
                    PrintDirect()
                End If
            End Sub
        End Class


        Private isVisible_Renamed As Boolean
        Private defaultReportInfo As IReportInfo
        Private reportInfo As IReportInfo
        Private actualReportInfo As IReportInfo

        Protected Property IsVisible() As Boolean
            Get
                Return isVisible_Renamed
            End Get
            Set(ByVal value As Boolean)
                isVisible_Renamed = value
                UpdateReport()
                If Not isVisible_Renamed Then
                    Me.reportInfo = Nothing
                End If
            End Set
        End Property
        Protected Overridable Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            Me.defaultReportInfo = reportInfo
            UpdateReport()
        End Sub
        Protected Overridable Sub ShowReport(ByVal reportInfo As IReportInfo)
            Me.reportInfo = reportInfo
            UpdateReport()
        End Sub
        Private Sub UpdateReport()
            UpdateReportCore(If(IsVisible, (If(reportInfo, defaultReportInfo)), Nothing))
        End Sub
        Protected Overridable Sub UpdateReportCore(ByVal actualReportInfo As IReportInfo)
            UnsubscribeFromParametersViewModel()
            Me.actualReportInfo = actualReportInfo
            SubscribeToParametersViewModel()
            If Me.actualReportInfo Is Nothing Then
                DestroyReport()
            Else
                CreateReport()
            End If
        End Sub
        Private Sub OnParametersViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            CreateReport()
        End Sub
        Private Sub CreateReport()
            Dim report As IReport = actualReportInfo.CreateReport()
            Dim model As PrintDirectXtraReportPreviewModel = TryCast(GetPreviewModel(), PrintDirectXtraReportPreviewModel)
            If model Is Nothing Then
                model = New PrintDirectXtraReportPreviewModel() With {.ZoomMode = New ZoomFitModeItem(ZoomFitMode.PageWidth)}
                SetPreviewModel(model)
            End If
            model.Report = report
            SetCustomSettingsViewModel(actualReportInfo.ParametersViewModel)
            report.PrintingSystemBase.ClearContent()
            report.CreateDocument(True)
        End Sub
        Private Sub DestroyReport()
            SetCustomSettingsViewModel(Nothing)
            SetPreviewModel(Nothing)
        End Sub
        Protected MustOverride Function GetPreviewModel() As IPreviewModel
        Protected MustOverride Sub SetPreviewModel(ByVal previewModel As IDocumentPreviewModel)
        Protected MustOverride Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
        Private ReadOnly Property ActualParametersViewModel() As Object
            Get
                Return If(Me.actualReportInfo Is Nothing, Nothing, Me.actualReportInfo.ParametersViewModel)
            End Get
        End Property
        Private Sub SubscribeToParametersViewModel()
            Dim parametersViewModel As INotifyPropertyChanged = TryCast(ActualParametersViewModel, INotifyPropertyChanged)
            If parametersViewModel IsNot Nothing Then
                AddHandler parametersViewModel.PropertyChanged, AddressOf OnParametersViewModelPropertyChanged
            End If
        End Sub
        Private Sub UnsubscribeFromParametersViewModel()
            Dim parametersViewModel As INotifyPropertyChanged = TryCast(ActualParametersViewModel, INotifyPropertyChanged)
            If parametersViewModel IsNot Nothing Then
                RemoveHandler parametersViewModel.PropertyChanged, AddressOf OnParametersViewModelPropertyChanged
            End If
        End Sub
        #Region "IReportService"
        Private Sub IReportService_SetDefaultReport(ByVal reportInfo As IReportInfo) Implements IReportService.SetDefaultReport
            SetDefaultReport(reportInfo)
        End Sub
        Private Sub IReportService_ShowReport(ByVal reportInfo As IReportInfo) Implements IReportService.ShowReport
            ShowReport(reportInfo)
        End Sub
        #End Region
    End Class
    Public Class BackstageDocumentPreviewReportService
        Inherits ReportServiceBase

        Public Shared ReadOnly BackstageViewIsOpenProperty As DependencyProperty = DependencyProperty.Register("BackstageViewIsOpen", GetType(Boolean), GetType(BackstageDocumentPreviewReportService), New PropertyMetadata(False, Sub(d, e) CType(d, BackstageDocumentPreviewReportService).OnBackstageViewIsOpenChanged()))
        Public Shared ReadOnly BackstageItemProperty As DependencyProperty = DependencyProperty.Register("BackstageItem", GetType(BackstageTabItem), GetType(BackstageDocumentPreviewReportService), New PropertyMetadata(Nothing, Sub(d, e) CType(d, BackstageDocumentPreviewReportService).OnBackstageItemChanged(CType(e.OldValue, BackstageItem), CType(e.NewValue, BackstageItem))))
        Public Shared ReadOnly BackstageDocumentPreviewProperty As DependencyProperty = DependencyProperty.Register("BackstageDocumentPreview", GetType(BackstageDocumentPreview), GetType(BackstageDocumentPreviewReportService))

        Public Property BackstageViewIsOpen() As Boolean
            Get
                Return CBool(GetValue(BackstageViewIsOpenProperty))
            End Get
            Set(ByVal value As Boolean)
                SetValue(BackstageViewIsOpenProperty, value)
            End Set
        End Property
        Public Property BackstageItem() As BackstageTabItem
            Get
                Return CType(GetValue(BackstageItemProperty), BackstageTabItem)
            End Get
            Set(ByVal value As BackstageTabItem)
                SetValue(BackstageItemProperty, value)
            End Set
        End Property
        Public Property BackstageDocumentPreview() As BackstageDocumentPreview
            Get
                Return CType(GetValue(BackstageDocumentPreviewProperty), BackstageDocumentPreview)
            End Get
            Set(ByVal value As BackstageDocumentPreview)
                SetValue(BackstageDocumentPreviewProperty, value)
            End Set
        End Property

        Protected Overrides Sub ShowReport(ByVal reportInfo As IReportInfo)
            MyBase.ShowReport(reportInfo)
            Dispatcher.BeginInvoke(CType(AddressOf OpenBackstageView, Action))
        End Sub
        Private Sub OpenBackstageView()
            BackstageItem.Backstage.SelectedTab = BackstageItem
            BackstageItem.Backstage.IsOpen = True
        End Sub
        Protected Overrides Sub UpdateReportCore(ByVal actualReportInfo As IReportInfo)
            MyBase.UpdateReportCore(actualReportInfo)
            BackstageItem.IsEnabled = actualReportInfo IsNot Nothing
        End Sub
        Private Sub OnBackstageViewIsOpenChanged()
            IsVisible = BackstageViewIsOpen
        End Sub
        Private Sub OnBackstageItemChanged(ByVal oldItem As BackstageItem, ByVal newItem As BackstageItem)
            If newItem IsNot Nothing Then
                newItem.IsEnabled = oldItem IsNot Nothing AndAlso oldItem.IsEnabled
            End If
        End Sub
        Protected Overrides Function GetPreviewModel() As IPreviewModel
            Return BackstageDocumentPreview.Model
        End Function
        Protected Overrides Sub SetPreviewModel(ByVal previewModel As IDocumentPreviewModel)
            BackstageDocumentPreview.Model = previewModel
        End Sub
        Protected Overrides Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
            BackstageDocumentPreview.CustomSettings = customSettingsViewModel
        End Sub
    End Class
    Public Class DocumentViewerReportService
        Inherits ReportServiceBase

        Private ReadOnly Property DocumentViewer() As DocumentViewer
            Get
                Return CType(AssociatedObject, DocumentViewer)
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            IsVisible = True
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            IsVisible = False
        End Sub
        Protected Overrides Function GetPreviewModel() As IPreviewModel
            Return DocumentViewer.Model
        End Function
        Protected Overrides Sub SetPreviewModel(ByVal previewModel As IDocumentPreviewModel)
            DocumentViewer.Model = previewModel
        End Sub
        Protected Overrides Sub SetCustomSettingsViewModel(ByVal customSettingsViewModel As Object)
        End Sub
    End Class
End Namespace
