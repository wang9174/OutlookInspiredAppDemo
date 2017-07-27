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
    ''' Represents the Tasks collection view model.
    ''' </summary>
    Partial Public Class EmployeeTaskCollectionViewModel
        Inherits CollectionViewModel(Of EmployeeTask, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of EmployeeTaskCollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeTaskCollectionViewModel
            Return ViewModelSource.Create(Function() New EmployeeTaskCollectionViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the EmployeeTaskCollectionViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeTaskCollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Tasks, Function(query) query.OrderByDescending(Function(x) x.DueDate))
        End Sub
    End Class
End Namespace
