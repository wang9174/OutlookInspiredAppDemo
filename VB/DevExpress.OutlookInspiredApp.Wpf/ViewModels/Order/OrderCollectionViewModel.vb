Imports System
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common.DataModel
Imports DevExpress.DevAV
Imports DevExpress.DevAV.Common.ViewModel
Namespace DevExpress.DevAV.ViewModels
  ''' <summary>
  ''' Represents the Orders collection view model.
  ''' </summary>
  Public Partial Class OrderCollectionViewModel
    Inherits CollectionViewModel(Of Order, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of OrderCollectionViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As OrderCollectionViewModel
      Return ViewModelSource.Create(Function() New OrderCollectionViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the OrderCollectionViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the OrderCollectionViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Orders, Function(ByVal query) query.Include(Function(ByVal x) x.Store).Include(Function(ByVal x) x.Customer).ActualOrders().OrderBy(Function(ByVal x) x.InvoiceNumber))
    End Sub
  End Class
End Namespace
