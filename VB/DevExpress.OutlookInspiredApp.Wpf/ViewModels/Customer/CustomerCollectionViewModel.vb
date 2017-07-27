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
  ''' Represents the Customers collection view model.
  ''' </summary>
  Public Partial Class CustomerCollectionViewModel
    Inherits CollectionViewModel(Of Customer, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of CustomerCollectionViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As CustomerCollectionViewModel
      Return ViewModelSource.Create(Function() New CustomerCollectionViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the CustomerCollectionViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the CustomerCollectionViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Customers, Function(ByVal query) query.OrderBy(Function(ByVal x) x.Name))
    End Sub
  End Class
End Namespace
