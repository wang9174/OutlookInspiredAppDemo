Imports System.Linq
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeViewModel

        Private contacts_Renamed As EmployeeContactsViewModel

        Private quickLetter_Renamed As EmployeeQuickLetterViewModel

        Private linksViewModel_Renamed As LinksViewModel

        Private employeeAssignedTasksDetails_Renamed As EmployeeTaskDetailsCollectionViewModel
        Public ReadOnly Property Contacts() As EmployeeContactsViewModel
            Get
                If contacts_Renamed Is Nothing Then
                    contacts_Renamed = EmployeeContactsViewModel.Create().SetParentViewModel(Me)
                End If
                Return contacts_Renamed
            End Get
        End Property
        Public ReadOnly Property EmployeeAssignedTasksDetails() As EmployeeTaskDetailsCollectionViewModel
            Get
                If employeeAssignedTasksDetails_Renamed Is Nothing Then
                    employeeAssignedTasksDetails_Renamed = EmployeeTaskDetailsCollectionViewModel.Create().SetParentViewModel(Me)
                End If
                Return employeeAssignedTasksDetails_Renamed
            End Get
        End Property
        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            Contacts.Entity = Entity
            QuickLetter.Entity = Entity
            SetDefaultReport(ReportInfoFactory.EmployeeProfile(Entity))
            EmployeeAssignedTasksDetails.UpdateFilter()
        End Sub

        Protected Overrides Function GetTitle() As String
            Return Entity.FullName
        End Function
        Public ReadOnly Property QuickLetter() As EmployeeQuickLetterViewModel
            Get
                If quickLetter_Renamed Is Nothing Then
                    quickLetter_Renamed = EmployeeQuickLetterViewModel.Create().SetParentViewModel(Me)
                End If
                Return quickLetter_Renamed
            End Get
        End Property
        Public Sub ShowMap()
            Dim mapViewModel = EmployeeCollectionViewModel.CreateEmployeeMapViewModel(Entity, Sub(destination)
                Entity.Address = destination
                Me.RaisePropertyChanged(Function(x) x.Entity)
            End Sub)
            DocumentManagerService.CreateDocument("NavigatorMapView", mapViewModel, Nothing, Me).Show()
        End Sub
        Protected ReadOnly Property DocumentManagerService() As IDocumentManagerService
            Get
                Return Me.GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property
        Public ReadOnly Property LinksViewModel() As LinksViewModel
            Get
                If linksViewModel_Renamed Is Nothing Then
                    linksViewModel_Renamed = DevExpress.DevAV.ViewModels.LinksViewModel.Create()
                End If
                Return linksViewModel_Renamed
            End Get
        End Property
        Public Sub ShowScheduler(ByVal title As String)
            MessageBoxService.Show("This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.", title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub
        Protected Overrides Function SaveCore() As Boolean
            If IsNew() AndAlso String.IsNullOrEmpty(Entity.FullName) Then
                Entity.FullName = String.Format("{0} {1}", Entity.FirstName, Entity.LastName)
            End If
            Return MyBase.SaveCore()
        End Function
        Public Sub PrintEmployeeProfile()
            ShowReport(ReportInfoFactory.EmployeeProfile(Entity))
        End Sub
        Public Function CanPrintEmployeeProfile() As Boolean
            Return Entity IsNot Nothing
        End Function
        Public Sub PrintSummaryReport()
            ShowReport(ReportInfoFactory.EmployeeSummary(Repository.ToList()))
        End Sub
        Public Sub PrintDirectory()
            ShowReport(ReportInfoFactory.EmployeeDirectory(Repository.ToList()))
        End Sub
        Public Sub PrintTaskList()
            ShowReport(ReportInfoFactory.EmployeeTaskList(UnitOfWork.Tasks.ToList()))
        End Sub

        Private Sub ShowReport(ByVal reportInfo As IReportInfo)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
        End Sub
        Private Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            If Me.IsInDesignMode() Then
                Return
            End If
            ExportService.SetDefaultReport(reportInfo)
            PrintService.SetDefaultReport(reportInfo)
        End Sub
        Private ReadOnly Property PrintService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("PrintService")
            End Get
        End Property
        Private ReadOnly Property ExportService() As IReportService
            Get
                Return Me.GetRequiredService(Of IReportService)("ExportService")
            End Get
        End Property
    End Class
End Namespace
