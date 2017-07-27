Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Spreadsheet

Namespace DevExpress.DevAV.Views.Customer
    Partial Public Class CustomerAnalysis
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            SubscribeEvents()
        End Sub

        Private Sub SubscribeEvents()
            AddHandler Loaded, AddressOf OnLoaded
            AddHandler spreadsheetControl.DocumentLoaded, AddressOf OnDocumentLoaded
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            LoadTemplate()
        End Sub
        Private Sub LoadTemplate()
            Using stream = AnalysisTemplatesHelper.GetAnalysisTemplate(AnalysisTemplate.CustomerSales)
                spreadsheetControl.LoadDocument(stream, DocumentFormat.Xlsm)
            End Using
        End Sub

        Private Sub OnDocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            LoadData()
        End Sub
        Private Sub LoadData()
            Dim ViewModel As CustomerAnalysisViewModel = DirectCast(Me.DataContext, CustomerAnalysisViewModel)
            spreadsheetControl.Document.BeginUpdate()
            Dim salesReportWorksheet = spreadsheetControl.Document.Worksheets("Sales Report")
            Dim salesReportItems = ViewModel.GetSalesReport().ToList()
            Dim frCustomers = salesReportItems.Select(Function(i) i.CustomerName).Distinct().OrderBy(Function(i) i).ToList()
            salesReportWorksheet.Import(frCustomers, 14, 1, True)
            For Each item As CustomersAnalysis.Item In salesReportItems
                Dim rowOffset As Integer = frCustomers.IndexOf(item.CustomerName)
                Dim columnOffset As Integer = CInt(AnalysisPeriod.MonthOffsetFromStart(item.Date) \ 12)
                If rowOffset < 0 OrElse columnOffset < 0 Then
                    Continue For
                End If
                salesReportWorksheet.Cells(14 + rowOffset, 3 + columnOffset * 2).SetValue(item.Total)
            Next item
            Dim salesDataWorksheet = spreadsheetControl.Document.Worksheets("Sales Data")
            Dim salesDataItems = ViewModel.GetSalesData().ToList()
            Dim states = salesDataItems.Select(Function(i) i.State).Distinct().OrderBy(Function(i) i).ToList()

            salesDataWorksheet.Import(ViewModel.GetStates(states), 5, 3, False)
            For Each item As CustomersAnalysis.Item In salesDataItems
                Dim rowOffset As Integer = AnalysisPeriod.MonthOffsetFromStart(item.Date)
                Dim columnOffset As Integer = states.IndexOf(item.State)
                If rowOffset < 0 OrElse columnOffset < 0 Then
                    Continue For
                End If
                salesDataWorksheet.Cells(6 + rowOffset, 3 + columnOffset).SetValue(item.Total)
            Next item
            spreadsheetControl.Document.Worksheets.ActiveWorksheet = salesReportWorksheet
            spreadsheetControl.Document.EndUpdate()
        End Sub
    End Class
End Namespace
