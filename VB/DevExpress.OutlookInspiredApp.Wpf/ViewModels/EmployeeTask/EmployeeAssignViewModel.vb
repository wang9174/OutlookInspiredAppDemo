Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV.Common.DataModel
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class EmployeeAssignViewModel
        Inherits SingleObjectViewModel(Of EmployeeTask, Long, IDevAVDbUnitOfWork)

        Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing) As EmployeeAssignViewModel
            Return ViewModelSource.Create(Function() New EmployeeAssignViewModel(unitOfWorkFactory))
        End Function
        Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Tasks, Function(x) x.Subject)
        End Sub

        Public Overridable Property EntityContextLookUpEmployees() As IEnumerable(Of Employee)
        Private allowEntityChange As Boolean

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            allowEntityChange = False
            EntityContextLookUpEmployees = Me.UnitOfWork.Employees.AsEnumerable()
            allowEntityChange = True
        End Sub
        Public Overridable Sub AssignedEmployeesChanged()
            If allowEntityChange Then
                Entity.AttachedCollectionsChanged = True
                Update()
                Entity.AttachedCollectionsChanged = False
            End If
        End Sub
        Protected Overrides Function GetTitle() As String
            Return "Assign task"
        End Function
        Protected Overrides Function TryClose() As Boolean
            Return True
        End Function
        <Command(CanExecuteMethodName := "CanSave")>
        Public Overridable Sub SaveEntity()
            If SaveCore() Then
                Close()
            End If
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub
    End Class
End Namespace
