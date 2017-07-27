Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System.Collections.Generic

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class CustomerCollectionViewModel
        Implements ISupportFiltering(Of Customer)

        Private Const title As String = "Edit Customers"
        Private Const text As String = "You can easily create custom edit forms using the 40+ controls that ship as part of the DevExpress Data Editors Library. To see what you can build, activate the Employees module."

        Private viewSettings_Renamed As ViewSettingsViewModel

        Public ReadOnly Property ViewSettings() As ViewSettingsViewModel
            Get
                If viewSettings_Renamed Is Nothing Then
                    viewSettings_Renamed = ViewSettingsViewModel.Create(CollectionViewKind.ListView, Me)
                End If
                Return viewSettings_Renamed
            End Get
        End Property
        Public Sub ShowSalesMap(ByVal customer As Customer)
            Dim mapViewModel As CustomerMapViewModel = ViewModelSource.Create(Function() New CustomerMapViewModel())
            Me.GetRequiredService(Of IDocumentManagerService)().CreateDocument("CitiesMapView", mapViewModel, customer.Id, Me).Show()
        End Sub
        Public Function CanShowSalesMap(ByVal customer As Customer) As Boolean
            Return customer IsNot Nothing
        End Function

        Public Sub PrintCustomerProfile()
            ShowReport(ReportInfoFactory.CusomerProfile(SelectedEntity))
        End Sub
        Public Function CanPrintCustomerProfile() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintDirectory()
            ShowReport(ReportInfoFactory.CustomerContactsDirectory(SelectedEntity))
        End Sub
        Public Function CanPrintDirectory() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintOrderDetailReport()
            ShowReport(ReportInfoFactory.CustomerSalesDetailReport(QueriesHelper.GetCustomerSaleDetails(SelectedEntity.Id, CreateUnitOfWork().OrderItems)))
        End Sub
        Public Function CanPrintOrderDetailReport() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintOrderSummaryReport()
            ShowReport(ReportInfoFactory.CustomerSalesSummaryReport(QueriesHelper.GetCustomerSaleOrderItemDetails(SelectedEntity.Id, CreateUnitOfWork().OrderItems)))
        End Sub
        Public Function CanPrintOrderSummaryReport() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintLocationsDirectory()
            ShowReport(ReportInfoFactory.CustomerLocationsDirectory(Entities))
        End Sub

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            SetDefaultReport(ReportInfoFactory.CusomerProfile(SelectedEntity))
        End Sub

        Private Sub ShowReport(ByVal reportInfo As IReportInfo)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
        End Sub
        Private Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
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

        Public Overrides Sub Edit(ByVal entity As Customer)
            ShowCustomerEditForm()
        End Sub
        Public Overrides Sub [New]()
            ShowCustomerEditForm()
        End Sub
        Private Sub ShowCustomerEditForm()
            MessageBoxService.Show(text, title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub

        Public Sub CreateCustomFilter()
            Messenger.Default.Send(New CreateCustomFilterMessage(Of Customer)())
        End Sub

        Public Sub ShowAnalysis()
            Dim documentManagerService As IDocumentManagerService = Me.GetRequiredService(Of IDocumentManagerService)("AnalysisWindowedDocumentUIService")
            documentManagerService.CreateDocument("CustomerAnalysis", Nothing, Me).Show()
        End Sub
        Public Overrides Sub Delete(ByVal entity As Customer)
            MessageBoxService.ShowMessage("To ensure data integrity, the Customers module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Customer", MessageButton.OK)
        End Sub
        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of Customer, Boolean)) Implements ISupportFiltering(Of Customer).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of Customer, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
