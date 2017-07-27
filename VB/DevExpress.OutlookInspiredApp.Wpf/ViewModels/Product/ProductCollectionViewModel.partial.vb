Imports System
Imports System.IO
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class ProductCollectionViewModel
        Implements ISupportFiltering(Of Product)


        Private viewSettings_Renamed As ViewSettingsViewModel

        Protected ReadOnly Property AnalysisWindowedDocumentUIService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)("AnalysisWindowedDocumentUIService")
            End Get
        End Property

        Public Overridable Property SelectedPdfStream() As Stream
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
        Public Overrides Sub Edit(ByVal entity As Product)
            ShowProductEditForm()
        End Sub
        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            If SelectedEntity IsNot Nothing Then
                SelectedPdfStream = SelectedEntity.Catalog(0).PdfStream
            End If
            SetDefaultReport(ReportInfoFactory.ProductProfile(SelectedEntity))
        End Sub

        Public Sub PrintOrderDetail()
            ShowReport(ReportInfoFactory.ProductOrders(SelectedEntity.OrderItems, CreateUnitOfWork().States.ToList()))
        End Sub
        Public Function CanPrintOrderDetail() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintSpecificationSummary()
            ShowReport(ReportInfoFactory.ProductProfile(SelectedEntity))
        End Sub
        Public Function CanPrintSpecificationSummary() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintSalesSummary()
            ShowReport(ReportInfoFactory.ProductSalesSummary(SelectedEntity.OrderItems))
        End Sub
        Public Function CanPrintSalesSummary() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintTopSalesperson()
            ShowReport(ReportInfoFactory.ProductTopSalesPerson(SelectedEntity.OrderItems))
        End Sub
        Public Function CanPrintTopSalesperson() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

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

        Public Sub ShowMap()
            Dim mapViewModel As ProductMapViewModel = ViewModelSource.Create(Function() New ProductMapViewModel())
            Me.GetRequiredService(Of IDocumentManagerService)().CreateDocument("CitiesMapView", mapViewModel, SelectedEntity.Id, Me).Show()
        End Sub
        Public Function CanShowMap() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Private Sub ShowProductEditForm()
            MessageBoxService.Show("You can easily create custom edit forms using the 40+ controls that ship as part of the DevExpress Data Editors Library. To see what you can build, activate the Employees module.", "Edit Products", MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub
        Public Sub CreateCustomFilter()
            Messenger.Default.Send(New CreateCustomFilterMessage(Of Product)())
        End Sub

        Public Sub ShowAnalysis()
            AnalysisWindowedDocumentUIService.CreateDocument("ProductAnalysis", Nothing, Me).Show()
        End Sub
        Public Overrides Sub Delete(ByVal entity As Product)
            MessageBoxService.ShowMessage("To ensure data integrity, the Products module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Product", MessageButton.OK)
        End Sub
        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of Product, Boolean)) Implements ISupportFiltering(Of Product).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of Product, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
