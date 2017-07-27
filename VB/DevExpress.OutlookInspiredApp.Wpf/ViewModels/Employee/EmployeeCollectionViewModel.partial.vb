Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeCollectionViewModel
        Implements ISupportFiltering(Of Employee)

        Private Shared ReadOnly Property HomeAddress() As String
            Get
                Return "505 N Brand Blvd, Glendale, CA 91203"
            End Get
        End Property
        Private Shared ReadOnly Property HomeLocation() As GeoPoint
            Get
                Return New GeoPoint(34.153454, -118.255111)
            End Get
        End Property
        Friend Shared Function CreateEmployeeMapViewModel(ByVal employee As Employee, ByVal applyDestination As Action(Of Address)) As NavigatorMapViewModel(Of Employee)
            Return NavigatorMapViewModel(Of Employee).Create(employee, HomeAddress, HomeLocation, employee.Address.ToString(), New GeoPoint(employee.Address.Latitude, employee.Address.Longitude), applyDestination)
        End Function


        Private entityPanelViewModel_Renamed As EmployeeContactsViewModel

        Private quickLetter_Renamed As EmployeeQuickLetterViewModel

        Private viewSettings_Renamed As ViewSettingsViewModel

        Public ReadOnly Property EntityPanelViewModel() As EmployeeContactsViewModel
            Get
                If entityPanelViewModel_Renamed Is Nothing Then
                    entityPanelViewModel_Renamed = EmployeeContactsViewModel.Create()
                End If
                Return entityPanelViewModel_Renamed
            End Get
        End Property
        Public ReadOnly Property ViewSettings() As ViewSettingsViewModel
            Get
                If viewSettings_Renamed Is Nothing Then
                    viewSettings_Renamed = ViewSettingsViewModel.Create(CollectionViewKind.CardView, Me)
                End If
                Return viewSettings_Renamed
            End Get
        End Property
        Public ReadOnly Property QuickLetter() As EmployeeQuickLetterViewModel
            Get
                If quickLetter_Renamed Is Nothing Then
                    quickLetter_Renamed = EmployeeQuickLetterViewModel.Create().SetParentViewModel(Me)
                End If
                Return quickLetter_Renamed
            End Get
        End Property
        Public Sub ShowMap()
            Dim mapViewModel = CreateEmployeeMapViewModel(SelectedEntity, Sub(destination)
                SelectedEntity.Address = destination
                Save(SelectedEntity)
            End Sub)
            Me.GetRequiredService(Of IDocumentManagerService)().CreateDocument("EmployeeMapView", mapViewModel, Nothing, Me).Show()
        End Sub
        Public Function CanShowMap() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintEmployeeProfile()
            ShowReport(ReportInfoFactory.EmployeeProfile(SelectedEntity))
        End Sub
        Public Function CanPrintEmployeeProfile() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function
        Public Sub PrintSummaryReport()
            ShowReport(ReportInfoFactory.EmployeeSummary(Entities))
        End Sub
        Public Sub PrintDirectory()
            ShowReport(ReportInfoFactory.EmployeeDirectory(Entities))
        End Sub
        Public Sub PrintTaskList()
            ShowReport(ReportInfoFactory.TaskListReport(CreateUnitOfWork().Tasks.ToList()))
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

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            QuickLetter.Entity = SelectedEntity
            If SelectedEntity IsNot Nothing Then
                EntityPanelViewModel.Entity = SelectedEntity
            End If
            SetDefaultReport(ReportInfoFactory.EmployeeProfile(SelectedEntity))
        End Sub
        Public Overrides Sub UpdateSelectedEntity()
            MyBase.UpdateSelectedEntity()
            QuickLetter.RaisePropertyChanged(Function(x) x.Entity)
            EntityPanelViewModel.RaisePropertyChanged(Function(x) x.Entity)
        End Sub
        Public Sub ShowScheduler(ByVal title As String)
            MessageBoxService.Show("This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.", title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub
        Public Sub CreateCustomFilter()
            Messenger.Default.Send(New CreateCustomFilterMessage(Of Employee)())
        End Sub

        Public Sub AddTask()
            Dim initializer As Action(Of EmployeeTask) = Sub(x) x.OwnerId = SelectedEntity.Id
            Dim document As IDocument = Me.GetRequiredService(Of IDocumentManagerService)().CreateDocument("EmployeeTaskView", Nothing, initializer, Me)
            Dim taskViewModel As EmployeeTaskViewModel = CType(document.Content, EmployeeTaskViewModel)
            taskViewModel.Entity.AssignedEmployees.Add(taskViewModel.EntityContextLookUpEmployees.Single(Function(x) x.Id = SelectedEntity.Id))
            taskViewModel.RaisePropertyChanged(Function(x) x.Entity)
            document.Show()
            Me.Refresh()
        End Sub
        Public Function CanAddTask() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of Employee, Boolean)) Implements ISupportFiltering(Of Employee).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of Employee, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
