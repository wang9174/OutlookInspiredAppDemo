Imports System
Imports System.Linq
Imports System.Data
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.DataModel
Imports DevExpress.DevAV.Common.DataModel.EntityFramework
Imports DevExpress.DevAV
Namespace DevExpress.DevAV.DevAVDbDataModel
  ''' <summary>
  ''' A DevAVDbUnitOfWork instance that represents the run-time implementation of the IDevAVDbUnitOfWork interface.
  ''' </summary>
  Public Class DevAVDbUnitOfWork
    Inherits DbUnitOfWork(Of DevAVDb)
    Implements IDevAVDbUnitOfWork
    Public Sub New(ByVal contextFactory As Func(Of DevAVDb))
      MyBase.New(contextFactory)
    End Sub
    Private ReadOnly Property Communications As IRepository(Of CustomerCommunication, long) Implements IDevAVDbUnitOfWork.Communications
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of CustomerCommunication)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property CustomerEmployees As IRepository(Of CustomerEmployee, long) Implements IDevAVDbUnitOfWork.CustomerEmployees
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of CustomerEmployee)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Customers As IRepository(Of Customer, long) Implements IDevAVDbUnitOfWork.Customers
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Customer)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property CustomerStores As IRepository(Of CustomerStore, long) Implements IDevAVDbUnitOfWork.CustomerStores
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of CustomerStore)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Crests As IRepository(Of Crest, long) Implements IDevAVDbUnitOfWork.Crests
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Crest)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Orders As IRepository(Of Order, long) Implements IDevAVDbUnitOfWork.Orders
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Order)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Employees As IRepository(Of Employee, long) Implements IDevAVDbUnitOfWork.Employees
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Employee)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Tasks As IRepository(Of EmployeeTask, long) Implements IDevAVDbUnitOfWork.Tasks
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of EmployeeTask)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Evaluations As IRepository(Of Evaluation, long) Implements IDevAVDbUnitOfWork.Evaluations
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Evaluation)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Pictures As IRepository(Of Picture, long) Implements IDevAVDbUnitOfWork.Pictures
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Picture)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Probations As IRepository(Of Probation, long) Implements IDevAVDbUnitOfWork.Probations
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Probation)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property OrderItems As IRepository(Of OrderItem, long) Implements IDevAVDbUnitOfWork.OrderItems
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of OrderItem)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Products As IRepository(Of Product, long) Implements IDevAVDbUnitOfWork.Products
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Product)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property ProductCatalogs As IRepository(Of ProductCatalog, long) Implements IDevAVDbUnitOfWork.ProductCatalogs
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of ProductCatalog)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property ProductImages As IRepository(Of ProductImage, long) Implements IDevAVDbUnitOfWork.ProductImages
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of ProductImage)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Quotes As IRepository(Of Quote, long) Implements IDevAVDbUnitOfWork.Quotes
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of Quote)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property QuoteItems As IRepository(Of QuoteItem, long) Implements IDevAVDbUnitOfWork.QuoteItems
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of QuoteItem)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property States As IRepository(Of State, StateEnum) Implements IDevAVDbUnitOfWork.States
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of State)(), Function(ByVal x) x.ShortName)
      End Get
    End Property
    Private ReadOnly Property TaskAttachedFiles As IRepository(Of TaskAttachedFile, long) Implements IDevAVDbUnitOfWork.TaskAttachedFiles
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of TaskAttachedFile)(), Function(ByVal x) x.Id)
      End Get
    End Property
    Private ReadOnly Property Version As IRepository(Of DatabaseVersion, long) Implements IDevAVDbUnitOfWork.Version
      Get
        Return GetRepository(Function(ByVal x) x.[Set](Of DatabaseVersion)(), Function(ByVal x) x.Id)
      End Get
    End Property
  End Class
End Namespace
