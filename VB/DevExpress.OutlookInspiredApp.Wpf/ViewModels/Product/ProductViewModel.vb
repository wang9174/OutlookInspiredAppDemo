Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common.DataModel
Imports DevExpress.DevAV
Imports DevExpress.DevAV.Common.ViewModel
Namespace DevExpress.DevAV.ViewModels
  ''' <summary>
  ''' Represents the single Product object view model.
  ''' </summary>
  Public Partial Class ProductViewModel
    Inherits SingleObjectViewModel(Of Product, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of ProductViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As ProductViewModel
      Return ViewModelSource.Create(Function() New ProductViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the ProductViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the ProductViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Products, Function(ByVal x) x.Name)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpEmployees As IEntitiesViewModel(Of Employee)
      Get
        Return GetLookUpEntitiesViewModel(Function(ByVal x As ProductViewModel) x.LookUpEmployees, Function(ByVal x) x.Employees)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Pictures for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpPictures As IEntitiesViewModel(Of Picture)
      Get
        Return GetLookUpEntitiesViewModel(Function(ByVal x As ProductViewModel) x.LookUpPictures, Function(ByVal x) x.Pictures)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductCatalog detail collection.
    ''' </summary>
    Public ReadOnly Property ProductCatalogDetails As CollectionViewModel(Of ProductCatalog, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Function(ByVal x As ProductViewModel) x.ProductCatalogDetails, Function(ByVal x) x.ProductCatalogs, Function(ByVal x) x.ProductId, Sub(ByVal x, ByVal key) x.ProductId = key)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductOrderItems detail collection.
    ''' </summary>
    Public ReadOnly Property ProductOrderItemsDetails As CollectionViewModel(Of OrderItem, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Function(ByVal x As ProductViewModel) x.ProductOrderItemsDetails, Function(ByVal x) x.OrderItems, Function(ByVal x) x.ProductId, Sub(ByVal x, ByVal key) x.ProductId = key)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the ProductImages detail collection.
    ''' </summary>
    Public ReadOnly Property ProductImagesDetails As CollectionViewModel(Of ProductImage, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Function(ByVal x As ProductViewModel) x.ProductImagesDetails, Function(ByVal x) x.ProductImages, Function(ByVal x) x.ProductId, Sub(ByVal x, ByVal key) x.ProductId = key)
      End Get
    End Property
  End Class
End Namespace
