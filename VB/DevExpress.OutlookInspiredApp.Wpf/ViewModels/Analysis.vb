Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV.DevAVDbDataModel

Namespace DevExpress.DevAV.ViewModels
    Public NotInheritable Class AnalysisPeriod

        Private Sub New()
        End Sub

        Public Shared ReadOnly Start As New Date(2011, 10, 1)
        Public Shared ReadOnly [End] As New Date(2014, 09, 30)
        Public Shared Function MonthOffsetFromStart(ByVal dateTime As Date) As Integer
            Return (dateTime.Year - Start.Year) * 12 + dateTime.Month - Start.Month
        End Function
        Public Class Item
            Public Property Total() As Decimal
            Public Property Year() As Integer
            Public Property Month() As Integer
            Public ReadOnly Property [Date]() As Date
                Get
                    Return New Date(Year, Month, 1)
                End Get
            End Property
        End Class
    End Class
    Public Module ProductsAnalysis
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetFinancialReport(ByVal UnitOfWork As IDevAVDbUnitOfWork) As IEnumerable(Of Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems
                             Join o In orders On oi.OrderId Equals o.Id
                             Where (o.OrderDate >= AnalysisPeriod.Start AndAlso o.OrderDate <= AnalysisPeriod.End)
                             Select New With {Key .Product = oi.Product, Key .Total = oi.Total, Key .FY = ((o.OrderDate.Year - AnalysisPeriod.Start.Year) * 12 + (o.OrderDate.Month - AnalysisPeriod.Start.Month)) \ 12}
            Return From oi In orderItems
                   Group oi By GroupKey = New With {Key oi.Product, Key oi.FY} Into g = Group
                   Select New Item With {.ProductName = GroupKey.Product.Name, .Year = AnalysisPeriod.Start.Year + GroupKey.FY, .Month = AnalysisPeriod.Start.Month, .Total = If(g.Select(Function(x) CType(x.Total, Decimal?)).Sum(), 0)}
        End Function
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetFinancialData(ByVal UnitOfWork As IDevAVDbUnitOfWork) As IEnumerable(Of Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems
                             Join o In orders On oi.OrderId Equals o.Id
                             Where (o.OrderDate >= AnalysisPeriod.Start AndAlso o.OrderDate <= AnalysisPeriod.End)
                             Select New With {Key .Product = oi.Product, Key .Date = o.OrderDate, Key .Total = oi.Total}
            Return From oi In orderItems
                   Group oi By GroupKey = New With {Key oi.Product.Category, Key oi.Date.Year, Key oi.Date.Month} Into g = Group
                   Select New Item With {.ProductCategory = GroupKey.Category, .Year = GroupKey.Year, .Month = GroupKey.Month, .Total = If(g.Select(Function(x) CType(x.Total, Decimal?)).Sum(), 0)}
        End Function
        Public Class Item
            Inherits AnalysisPeriod.Item

            Public Property ProductName() As String
            Public Property ProductCategory() As ProductCategory
        End Class
    End Module
    Public Module CustomersAnalysis
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetSalesReport(ByVal UnitOfWork As IDevAVDbUnitOfWork) As IEnumerable(Of Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems
                             Join o In orders On oi.OrderId Equals o.Id
                             Where (o.OrderDate >= AnalysisPeriod.Start AndAlso o.OrderDate <= AnalysisPeriod.End)
                             Select New With {Key .Customer = o.Customer, Key .Total = oi.Total, Key .FY = ((o.OrderDate.Year - AnalysisPeriod.Start.Year) * 12 + (o.OrderDate.Month - AnalysisPeriod.Start.Month)) \ 12}
            Return From oi In orderItems
                   Group oi By GroupKey = New With {Key oi.Customer, Key oi.FY} Into g = Group
                   Select New Item With {.CustomerName = GroupKey.Customer.Name, .Year = AnalysisPeriod.Start.Year + GroupKey.FY, .Month = AnalysisPeriod.Start.Month, .Total = If(g.Select(Function(o) CType(o.Total, Decimal?)).Sum(), 0)}
        End Function
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetSalesData(ByVal UnitOfWork As IDevAVDbUnitOfWork) As IEnumerable(Of Item)
            Dim orders = UnitOfWork.Orders
            Dim orderItems = From oi In UnitOfWork.OrderItems
                             Join o In orders On oi.OrderId Equals o.Id
                             Where (o.OrderDate >= AnalysisPeriod.Start AndAlso o.OrderDate <= AnalysisPeriod.End)
                             Select New With {Key .State = o.Store.Address.State, Key .Date = o.OrderDate, Key .Total = oi.Total}
            Return From oi In orderItems
                   Group oi By GroupKey = New With {Key oi.State, Key oi.Date.Year, Key oi.Date.Month} Into g = Group
                   Select New Item With {.State = GroupKey.State, .Year = GroupKey.Year, .Month = GroupKey.Month, .Total = If(g.Select(Function(o) CType(o.Total, Decimal?)).Sum(), 0)}
        End Function
        Public Class Item
            Inherits AnalysisPeriod.Item

            Public Property CustomerName() As String
            Public Property State() As StateEnum
        End Class
    End Module
End Namespace
