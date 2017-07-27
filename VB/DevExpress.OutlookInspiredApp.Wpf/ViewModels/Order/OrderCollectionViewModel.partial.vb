Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class OrderCollectionViewModel
        Implements ISupportFiltering(Of Order)


        Private viewSettings_Renamed As ViewSettingsViewModel

        Public ReadOnly Property ViewSettings() As ViewSettingsViewModel
            Get
                If viewSettings_Renamed Is Nothing Then
                    viewSettings_Renamed = ViewSettingsViewModel.Create(CollectionViewKind.ListView, Me)
                End If
                Return viewSettings_Renamed
            End Get
        End Property
        Public Overrides Sub [New]()
            ShowProductEditForm()
        End Sub
        Public Overrides Sub Edit(ByVal entity As Order)
            ShowProductEditForm()
        End Sub

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            SetDefaultReport(ReportInfoFactory.SalesInvoice(SelectedEntity))
        End Sub

        Public Sub PrintInvoice()
            ShowReport(ReportInfoFactory.SalesInvoice(SelectedEntity))
        End Sub
        Public Function CanPrintInvoice() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintSummaryReport()
            ShowReport(ReportInfoFactory.SalesOrdersSummaryReport(QueriesHelper.GetSaleSummaries(CreateUnitOfWork().OrderItems)))
        End Sub

        Public Sub PrintSalesAnalysisReport()
            ShowReport(ReportInfoFactory.SalesAnalysisReport(QueriesHelper.GetSaleAnalysis(CreateUnitOfWork().OrderItems)))
        End Sub

        Private Sub ShowReport(ByVal reportInfo As IReportInfo)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
        End Sub
        Private Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            Me.GetRequiredService(Of IReportService)("DocumentViewerService").SetDefaultReport(reportInfo)
            If Me.IsInDesignMode() Then
                Return
            End If
            ExportService.SetDefaultReport(reportInfo)
            PrintService.SetDefaultReport(reportInfo)
        End Sub
        Private ReadOnly Property PrintService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("PrintService")
            End Get
        End Property
        Private ReadOnly Property ExportService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("ExportService")
            End Get
        End Property

        Private ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property

        Public Sub ShowMap()
            Dim mapViewModel = NavigatorMapViewModel(Of CustomerStore).Create(SelectedEntity.Store, SelectedEntity.Customer.HomeOffice.ToString(), New GeoPoint(SelectedEntity.Customer.HomeOffice.Latitude, SelectedEntity.Customer.HomeOffice.Longitude), SelectedEntity.Store.Address.ToString(), New GeoPoint(SelectedEntity.Store.Address.Latitude, SelectedEntity.Store.Address.Longitude))
            DocumentManagerService.CreateDocument("OrderMapView", mapViewModel, Nothing, Me).Show()
        End Sub
        Public Function CanShowMap() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Private Sub ShowProductEditForm()
            MessageBoxService.Show("You can easily create custom edit forms using the 40+ controls that ship as part of the DevExpress Data Editors Library. To see what you can build, activate the Employees module.", "Edit Sales", MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub
        Public Sub CreateCustomFilter()
            Messenger.Default.Send(New CreateCustomFilterMessage(Of Order)())
        End Sub

        Public Sub QuickLetter(ByVal templateName As String)
            Dim key? As Long = If(SelectedEntity.Customer.Employees.FirstOrDefault() Is Nothing, DirectCast(Nothing, Long?), SelectedEntity.Customer.Employees.FirstOrDefault().Id)

            Dim mailMergeViewModel_Renamed = MailMergeViewModel(Of CustomerEmployee, LinksViewModel).Create(unitOfWorkFactory, Function(x) x.CustomerEmployees, key, templateName, LinksViewModel.Create())
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel_Renamed, Nothing, Me).Show()
        End Sub
        Public Function CanQuickLetter(ByVal templateName As String) As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Overrides Sub Delete(ByVal entity As Order)
            MessageBoxService.ShowMessage("To ensure data integrity, the Sales module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Sale", MessageButton.OK)
        End Sub
        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of Order, Boolean)) Implements ISupportFiltering(Of Order).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of Order, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
