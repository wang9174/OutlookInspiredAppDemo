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
  ''' IDevAVDbUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
  ''' </summary>
  Public Interface IDevAVDbUnitOfWork
    Inherits IUnitOfWork    
    ''' <summary>
    ''' The CustomerCommunication entities repository.
    ''' </summary>
    ReadOnly Property Communications As IRepository(Of CustomerCommunication, long)    
    ''' <summary>
    ''' The CustomerEmployee entities repository.
    ''' </summary>
    ReadOnly Property CustomerEmployees As IRepository(Of CustomerEmployee, long)    
    ''' <summary>
    ''' The Customer entities repository.
    ''' </summary>
    ReadOnly Property Customers As IRepository(Of Customer, long)    
    ''' <summary>
    ''' The CustomerStore entities repository.
    ''' </summary>
    ReadOnly Property CustomerStores As IRepository(Of CustomerStore, long)    
    ''' <summary>
    ''' The Crest entities repository.
    ''' </summary>
    ReadOnly Property Crests As IRepository(Of Crest, long)    
    ''' <summary>
    ''' The Order entities repository.
    ''' </summary>
    ReadOnly Property Orders As IRepository(Of Order, long)    
    ''' <summary>
    ''' The Employee entities repository.
    ''' </summary>
    ReadOnly Property Employees As IRepository(Of Employee, long)    
    ''' <summary>
    ''' The EmployeeTask entities repository.
    ''' </summary>
    ReadOnly Property Tasks As IRepository(Of EmployeeTask, long)    
    ''' <summary>
    ''' The Evaluation entities repository.
    ''' </summary>
    ReadOnly Property Evaluations As IRepository(Of Evaluation, long)    
    ''' <summary>
    ''' The Picture entities repository.
    ''' </summary>
    ReadOnly Property Pictures As IRepository(Of Picture, long)    
    ''' <summary>
    ''' The Probation entities repository.
    ''' </summary>
    ReadOnly Property Probations As IRepository(Of Probation, long)    
    ''' <summary>
    ''' The OrderItem entities repository.
    ''' </summary>
    ReadOnly Property OrderItems As IRepository(Of OrderItem, long)    
    ''' <summary>
    ''' The Product entities repository.
    ''' </summary>
    ReadOnly Property Products As IRepository(Of Product, long)    
    ''' <summary>
    ''' The ProductCatalog entities repository.
    ''' </summary>
    ReadOnly Property ProductCatalogs As IRepository(Of ProductCatalog, long)    
    ''' <summary>
    ''' The ProductImage entities repository.
    ''' </summary>
    ReadOnly Property ProductImages As IRepository(Of ProductImage, long)    
    ''' <summary>
    ''' The Quote entities repository.
    ''' </summary>
    ReadOnly Property Quotes As IRepository(Of Quote, long)    
    ''' <summary>
    ''' The QuoteItem entities repository.
    ''' </summary>
    ReadOnly Property QuoteItems As IRepository(Of QuoteItem, long)    
    ''' <summary>
    ''' The State entities repository.
    ''' </summary>
    ReadOnly Property States As IRepository(Of State, StateEnum)    
    ''' <summary>
    ''' The Attached files repository.
    ''' </summary>
    ReadOnly Property TaskAttachedFiles As IRepository(Of TaskAttachedFile, long)    
    ''' <summary>
    ''' The DatabaseVersion entities repository.
    ''' </summary>
    ReadOnly Property Version As IRepository(Of DatabaseVersion, long)
  End Interface
End Namespace
