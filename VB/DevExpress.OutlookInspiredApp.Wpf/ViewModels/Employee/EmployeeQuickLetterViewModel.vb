Imports System
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class EmployeeQuickLetterViewModel
        Public Shared Function Create() As EmployeeQuickLetterViewModel
            Return ViewModelSource.Create(Function() New EmployeeQuickLetterViewModel())
        End Function
        Protected Sub New()
        End Sub
        Public Overridable Property Entity() As Employee
        Protected ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property
        Public Sub ShowMailMerge()
            QuickLetter(String.Empty)
        End Sub
        Public Sub QuickLetter(ByVal templateName As String)

            Dim mailMergeViewModel_Renamed = MailMergeViewModel(Of Employee, LinksViewModel).Create(UnitOfWorkSource.GetUnitOfWorkFactory(), Function(x) x.Employees,If(Entity Is Nothing, CType(Nothing, Long?), Entity.Id), templateName, LinksViewModel.Create())
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel_Renamed, Nothing, Me).Show()
        End Sub
        Public Function CanQuickLetter(ByVal templateName As String) As Boolean
            Return Entity IsNot Nothing
        End Function
        Protected Overridable Sub OnEntityChanged()
            Me.RaiseCanExecuteChanged(Sub(x) x.QuickLetter(""))
        End Sub
    End Class
End Namespace
