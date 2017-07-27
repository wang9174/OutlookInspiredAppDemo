Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.Reports
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.ViewModels
    Public NotInheritable Class ReportInfoFactory

        Private Sub New()
        End Sub

        #Region "Employee"
        Public Shared Function EmployeeTaskList(ByVal tasks As IEnumerable(Of EmployeeTask)) As IReportInfo
            Return GetReportInfo(SortByDateViewModel.Create(), Function(p) ReportFactory.EmployeeTaskList(tasks, p.SortDirection = SortByDatePrintMode.SortByDueDate))
        End Function

        Public Shared Function EmployeeProfile(ByVal employee As Employee) As IReportInfo
            Return GetReportInfo(EmployeeEvaluationsPrintModeViewModel.Create(), Function(p)If(employee Is Nothing, Nothing, ReportFactory.EmployeeProfile(employee, p.EmployeeEvaluationsPrintMode <> EmployeeEvaluationsPrintMode.ExcludeEvaluations)))
        End Function

        Public Shared Function EmployeeSummary(ByVal employees As IEnumerable(Of Employee)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.EmployeeSummary(employees, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function

        Public Shared Function EmployeeDirectory(ByVal employees As IEnumerable(Of Employee)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.EmployeeDirectory(employees, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function
        #End Region

        #Region "Customer"
        Public Shared Function CusomerProfile(ByVal customer As Customer) As IReportInfo
            Return GetReportInfo(CustomerContactsPrintModeViewModel.Create(), Function(p)If(customer Is Nothing, Nothing, ReportFactory.CustomerProfile(customer, p.CustomerContactsPrintMode <> CustomerContactsPrintMode.ExcludeContacts)))
        End Function
        Public Shared Function CustomerContactsDirectory(ByVal customer As Customer) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.CustomerContactsDirectory(customer.Employees, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function
        Public Shared Function CustomerSalesDetail(ByVal orders As IEnumerable(Of Order)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesDetail(orders, orders.SelectMany(Function(x) x.OrderItems).ToArray(), p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function CustomerSalesDetailReport(ByVal orders As IEnumerable(Of CustomerSaleDetailOrderInfo)) As IReportInfo
            Dim orderItems = orders.SelectMany(Function(x) x.OrderItems).ToArray()
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesDetailReport(orders, orderItems, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function CustomerSalesSummary(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesSummary(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function CustomerSalesSummaryReport(ByVal sales As IEnumerable(Of CustomerSaleDetailOrderItemInfo)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.CustomerSalesSummaryReport(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function CustomerLocationsDirectory(ByVal customers As IEnumerable(Of Customer)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.CustomerLocationsDirectory(customers, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function
        #End Region

        #Region "Order"
        Public Shared Function SalesInvoice(ByVal order As Order) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.SalesInvoice(order, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function
        Public Shared Function SalesOrdersSummary(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.SalesOrdersSummary(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function SalesOrdersSummaryReport(ByVal sales As IEnumerable(Of SaleSummaryInfo)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.SalesOrdersSummaryReport(sales, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function SalesAnalysis(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SelectYearsViewModel.Create(), Function(p) ReportFactory.SalesAnalysis(sales, p.Years))
        End Function
        Public Shared Function SalesAnalysisReport(ByVal sales As IEnumerable(Of SaleAnalisysInfo)) As IReportInfo
            Return GetReportInfo(SelectYearsViewModel.Create(), Function(p) ReportFactory.SalesAnalysisReport(sales, p.Years))
        End Function
        #End Region

        #Region "Task"
        Public Shared Function TaskListReport(ByVal tasks As IEnumerable(Of EmployeeTask)) As IReportInfo
            Return GetReportInfo(SortByDateViewModel.Create(), Function(p) ReportFactory.TaskListReport(tasks, p.SortDirection = SortByDatePrintMode.SortByDueDate))
        End Function

        Public Shared Function TaskDetailReport(ByVal task As EmployeeTask) As IReportInfo
            Return New ParameterlessReportInfo(ReportFactory.TaskDetailReport(task))
        End Function
        #End Region

        #Region "Product"
        Public Shared Function ProductProfile(ByVal product As Product) As IReportInfo
            Return GetReportInfo(ProductImagesPrintModeViewModel.Create(), Function(p) ReportFactory.ProductProfile(product, p.ProductImagesPrintMode = ProductImagesPrintMode.DisplayProductImages))
        End Function
        Public Shared Function ProductOrders(ByVal sales As IEnumerable(Of OrderItem), ByVal states As IList(Of State)) As IReportInfo
            Return GetReportInfo(SortByAndDateRangeViewModel.Create(), Function(p) ReportFactory.ProductOrders(sales, states, p.SortDirection = SortByPrintMode.SortByOrderDate, p.FromDate, p.ToDate))
        End Function
        Public Shared Function ProductSalesSummary(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SelectYearsViewModel.Create(), Function(p) ReportFactory.ProductSalesSummary(sales, p.Years))
        End Function
        Public Shared Function ProductTopSalesPerson(ByVal sales As IEnumerable(Of OrderItem)) As IReportInfo
            Return GetReportInfo(SortDirectionViewModel.Create(), Function(p) ReportFactory.ProductTopSalesPerson(sales, p.SortDirection = SortOrderPrintMode.AscendingOrder))
        End Function
        #End Region

        Private Shared Function GetReportInfo(Of TParametersViewModel)(ByVal parametersViewModel As TParametersViewModel, ByVal reportFactory As Func(Of TParametersViewModel, IReport)) As IReportInfo
            Return New ReportInfo(Of TParametersViewModel)(parametersViewModel, reportFactory)
        End Function
    End Class

    Public Class SortDirectionViewModel
        Public Shared Function Create() As SortDirectionViewModel
            Return ViewModelSource.Create(Function() New SortDirectionViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property SortDirection() As SortOrderPrintMode
    End Class

    Public Class SortDirectionAndDateRangeViewModel
        Inherits SortDirectionViewModel

        Public Shadows Shared Function Create() As SortDirectionAndDateRangeViewModel
            Return ViewModelSource.Create(Function() New SortDirectionAndDateRangeViewModel())
        End Function
        Protected Sub New()
            FromDate = New Date(2011, 1, 1)
            ToDate = New Date(2013, 1, 1)
        End Sub
        Public Overridable Property ToDate() As Date
        Public Overridable Property FromDate() As Date
    End Class

    Public Class SortByDateViewModel
        Public Shared Function Create() As SortByDateViewModel
            Return ViewModelSource.Create(Function() New SortByDateViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property SortDirection() As SortByDatePrintMode
    End Class

    Public Class SortByViewModel
        Public Shared Function Create() As SortByViewModel
            Return ViewModelSource.Create(Function() New SortByViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property SortDirection() As SortByPrintMode
    End Class

    Public Class SortByAndDateRangeViewModel
        Inherits SortByViewModel

        Public Shadows Shared Function Create() As SortByAndDateRangeViewModel
            Return ViewModelSource.Create(Function() New SortByAndDateRangeViewModel())
        End Function
        Protected Sub New()
            FromDate = New Date(2013, 1, 1)
            ToDate = New Date(2015, 6, 1)
        End Sub
        Public Overridable Property ToDate() As Date
        Public Overridable Property FromDate() As Date
    End Class

    Public Class EmployeeEvaluationsPrintModeViewModel
        Public Shared Function Create() As EmployeeEvaluationsPrintModeViewModel
            Return ViewModelSource.Create(Function() New EmployeeEvaluationsPrintModeViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property EmployeeEvaluationsPrintMode() As EmployeeEvaluationsPrintMode
    End Class

    Public Class CustomerContactsPrintModeViewModel
        Public Shared Function Create() As CustomerContactsPrintModeViewModel
            Return ViewModelSource.Create(Function() New CustomerContactsPrintModeViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property CustomerContactsPrintMode() As CustomerContactsPrintMode
    End Class

    Public Class SelectYearsViewModel
        Public Shared Function Create() As SelectYearsViewModel
            Return ViewModelSource.Create(Function() New SelectYearsViewModel())
        End Function
        Protected Sub New()
            AvailableYears = New List(Of String)() From {"2011", "2012", "2013", "2014", "2015"}
            Years = "2013,2014,2015"
        End Sub

        Public Property AvailableYears() As List(Of String)
        Public Overridable Property Years() As String
    End Class

    Public Class ProductImagesPrintModeViewModel
        Public Shared Function Create() As ProductImagesPrintModeViewModel
            Return ViewModelSource.Create(Function() New ProductImagesPrintModeViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property ProductImagesPrintMode() As ProductImagesPrintMode
    End Class

    Public Enum EmployeeEvaluationsPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-exclude-evaluations-32.png")>
        ExcludeEvaluations
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-include-evaluations-32.png")>
        IncludeEvaluations
    End Enum
    Public Enum CustomerContactsPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-include-evaluations-32.png")>
        IncludeContacts
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-exclude-evaluations-32.png")>
        ExcludeContacts
    End Enum
    Public Enum ProductImagesPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-show-product-img-32.png")>
        DisplayProductImages
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-Hide-product-img-32.png")>
        HideProductImages
    End Enum
    Public Enum SortOrderPrintMode
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-sort-ascending-32.png")>
        AscendingOrder
        <Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-sort-descinding-32.png")>
        DescencingOrder
    End Enum
    Public Enum SortByDatePrintMode
        <Display(Name := "Sort by Due Date"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-due-date-32.png")>
        SortByDueDate
        <Display(Name := "Sort by Start Date"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-print-start-date-32.png")>
        SortByStartDate
    End Enum
    Public Enum SortByPrintMode
        <Display(Name := "Sort by Order Date"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-Sort-by-OrderDate-32.png")>
        SortByOrderDate
        <Display(Name := "Sort by Invoice #"), Image("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-Sort-by-Invoice-32.png")>
        SortByInvoice
    End Enum
End Namespace
