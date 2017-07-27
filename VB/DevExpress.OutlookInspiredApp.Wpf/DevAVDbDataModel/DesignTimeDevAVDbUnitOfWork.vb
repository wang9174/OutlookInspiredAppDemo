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
  ''' A DevAVDbDesignTimeUnitOfWork instance that represents the design-time implementation of the IDevAVDbUnitOfWork interface.
  ''' </summary>
  Public Class DevAVDbDesignTimeUnitOfWork
    Inherits DesignTimeUnitOfWork
    Implements IDevAVDbUnitOfWork    
    ''' <summary>
    ''' Initializes a new instance of the DevAVDbDesignTimeUnitOfWork class.
    ''' </summary>
    Public Sub New()
    End Sub
    Private ReadOnly Property Communications As IRepository(Of CustomerCommunication, long) Implements IDevAVDbUnitOfWork.Communications
      Get
        Return GetRepository(Function(ByVal x As CustomerCommunication) x.Id)
      End Get
    End Property
    Private ReadOnly Property CustomerEmployees As IRepository(Of CustomerEmployee, long) Implements IDevAVDbUnitOfWork.CustomerEmployees
      Get
        Return GetRepository(Function(ByVal x As CustomerEmployee) x.Id)
      End Get
    End Property
    Private ReadOnly Property Customers As IRepository(Of Customer, long) Implements IDevAVDbUnitOfWork.Customers
      Get
        Return GetRepository(Function(ByVal x As Customer) x.Id)
      End Get
    End Property
    Private ReadOnly Property CustomerStores As IRepository(Of CustomerStore, long) Implements IDevAVDbUnitOfWork.CustomerStores
      Get
        Return GetRepository(Function(ByVal x As CustomerStore) x.Id)
      End Get
    End Property
    Private ReadOnly Property Crests As IRepository(Of Crest, long) Implements IDevAVDbUnitOfWork.Crests
      Get
        Return GetRepository(Function(ByVal x As Crest) x.Id)
      End Get
    End Property
    Private ReadOnly Property Orders As IRepository(Of Order, long) Implements IDevAVDbUnitOfWork.Orders
      Get
        Return GetRepository(Function(ByVal x As Order) x.Id)
      End Get
    End Property
    Private ReadOnly Property Employees As IRepository(Of Employee, long) Implements IDevAVDbUnitOfWork.Employees
      Get
        Return GetRepository(Function(ByVal x As Employee) x.Id)
      End Get
    End Property
    Private ReadOnly Property Tasks As IRepository(Of EmployeeTask, long) Implements IDevAVDbUnitOfWork.Tasks
      Get
        Return GetRepository(Function(ByVal x As EmployeeTask) x.Id)
      End Get
    End Property
    Private ReadOnly Property Evaluations As IRepository(Of Evaluation, long) Implements IDevAVDbUnitOfWork.Evaluations
      Get
        Return GetRepository(Function(ByVal x As Evaluation) x.Id)
      End Get
    End Property
    Private ReadOnly Property Pictures As IRepository(Of Picture, long) Implements IDevAVDbUnitOfWork.Pictures
      Get
        Return GetRepository(Function(ByVal x As Picture) x.Id)
      End Get
    End Property
    Private ReadOnly Property Probations As IRepository(Of Probation, long) Implements IDevAVDbUnitOfWork.Probations
      Get
        Return GetRepository(Function(ByVal x As Probation) x.Id)
      End Get
    End Property
    Private ReadOnly Property OrderItems As IRepository(Of OrderItem, long) Implements IDevAVDbUnitOfWork.OrderItems
      Get
        Return GetRepository(Function(ByVal x As OrderItem) x.Id)
      End Get
    End Property
    Private ReadOnly Property Products As IRepository(Of Product, long) Implements IDevAVDbUnitOfWork.Products
      Get
        Return GetRepository(Function(ByVal x As Product) x.Id)
      End Get
    End Property
    Private ReadOnly Property ProductCatalogs As IRepository(Of ProductCatalog, long) Implements IDevAVDbUnitOfWork.ProductCatalogs
      Get
        Return GetRepository(Function(ByVal x As ProductCatalog) x.Id)
      End Get
    End Property
    Private ReadOnly Property ProductImages As IRepository(Of ProductImage, long) Implements IDevAVDbUnitOfWork.ProductImages
      Get
        Return GetRepository(Function(ByVal x As ProductImage) x.Id)
      End Get
    End Property
    Private ReadOnly Property Quotes As IRepository(Of Quote, long) Implements IDevAVDbUnitOfWork.Quotes
      Get
        Return GetRepository(Function(ByVal x As Quote) x.Id)
      End Get
    End Property
    Private ReadOnly Property QuoteItems As IRepository(Of QuoteItem, long) Implements IDevAVDbUnitOfWork.QuoteItems
      Get
        Return GetRepository(Function(ByVal x As QuoteItem) x.Id)
      End Get
    End Property
    Private ReadOnly Property States As IRepository(Of State, StateEnum) Implements IDevAVDbUnitOfWork.States
      Get
        Return GetRepository(Function(ByVal x As State) x.ShortName)
      End Get
    End Property
    Private ReadOnly Property TaskAttachedFiles As IRepository(Of TaskAttachedFile, long) Implements IDevAVDbUnitOfWork.TaskAttachedFiles
      Get
        Return GetRepository(Function(ByVal x As TaskAttachedFile) x.Id)
      End Get
    End Property
    Private ReadOnly Property Version As IRepository(Of DatabaseVersion, long) Implements IDevAVDbUnitOfWork.Version
      Get
        Return GetRepository(Function(ByVal x As DatabaseVersion) x.Id)
      End Get
    End Property
  End Class
End Namespace
