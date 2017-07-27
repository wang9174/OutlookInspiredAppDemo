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
  ''' Represents the single Employee object view model.
  ''' </summary>
  Public Partial Class EmployeeViewModel
    Inherits SingleObjectViewModel(Of Employee, long, IDevAVDbUnitOfWork)    
    ''' <summary>
    ''' Creates a new instance of EmployeeViewModel as a POCO view model.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeViewModel
      Return ViewModelSource.Create(Function() New EmployeeViewModel(unitOfWorkFactory))
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the EmployeeViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeViewModel type without the POCO proxy factory.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
      MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(ByVal x) x.Employees, Function(ByVal x) x.FullName)
    End Sub    
    ''' <summary>
    ''' The view model that contains a look-up collection of Pictures for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpPictures As IEntitiesViewModel(Of Picture)
      Get
        Return GetLookUpEntitiesViewModel(Function(ByVal x As EmployeeViewModel) x.LookUpPictures, Function(ByVal x) x.Pictures)
      End Get
    End Property    
    ''' <summary>
    ''' The view model that contains a look-up collection of Probations for the corresponding navigation property in the view.
    ''' </summary>
    Public ReadOnly Property LookUpProbations As IEntitiesViewModel(Of Probation)
      Get
        Return GetLookUpEntitiesViewModel(Function(ByVal x As EmployeeViewModel) x.LookUpProbations, Function(ByVal x) x.Probations)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeOwnedTasks detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeOwnedTasksDetails As CollectionViewModel(Of EmployeeTask, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Function(ByVal x As EmployeeViewModel) x.EmployeeOwnedTasksDetails, Function(ByVal x) x.Tasks, Function(ByVal x) x.OwnerId, Sub(ByVal x, ByVal key) x.OwnerId = key)
      End Get
    End Property    
    ''' <summary>
    ''' The view model for the EmployeeEvaluations detail collection.
    ''' </summary>
    Public ReadOnly Property EmployeeEvaluationsDetails As CollectionViewModel(Of Evaluation, long, IDevAVDbUnitOfWork)
      Get
        Return GetDetailsCollectionViewModel(Function(ByVal x As EmployeeViewModel) x.EmployeeEvaluationsDetails, Function(ByVal x) x.Evaluations, Function(ByVal x) x.EmployeeId, Sub(ByVal x, ByVal key) x.EmployeeId = key)
      End Get
    End Property
  End Class
End Namespace
