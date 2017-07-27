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
    ''' Represents the single EmployeeTask object view model.
    ''' </summary>
    Partial Public Class EmployeeTaskViewModel
        Inherits SingleObjectViewModel(Of EmployeeTask, Long, IDevAVDbUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of EmployeeTaskViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeTaskViewModel
            Return ViewModelSource.Create(Function() New EmployeeTaskViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the EmployeeTaskViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the EmployeeTaskViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Tasks, Function(x) x.Subject)
        End Sub

        ''' <summary>
  ''' The view model that contains a look-up collection of CustomerEmployees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpCustomerEmployees() As IEntitiesViewModel(Of CustomerEmployee)
            Get
                Return GetLookUpEntitiesViewModel(Function(x As EmployeeTaskViewModel) x.LookUpCustomerEmployees, Function(x) x.CustomerEmployees)
            End Get
        End Property

        ''' <summary>
  ''' The view model that contains a look-up collection of Employees for the corresponding navigation property in the view.
        ''' </summary>
        Public ReadOnly Property LookUpEmployees() As IEntitiesViewModel(Of Employee)
            Get
                Return GetLookUpEntitiesViewModel(Function(x As EmployeeTaskViewModel) x.LookUpEmployees, Function(x) x.Employees)
            End Get
        End Property
    End Class
End Namespace
