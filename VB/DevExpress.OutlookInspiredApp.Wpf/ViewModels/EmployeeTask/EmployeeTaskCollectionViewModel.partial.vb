Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Windows.Media
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.DevAV.Common
Imports DevExpress.DevAV.Common.DataModel
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Partial Public Class EmployeeTaskCollectionViewModel
        Implements ISupportFiltering(Of EmployeeTask), IFilterTreeViewModelContainer(Of EmployeeTask, Long)

        Private ReadOnly Property Repository() As IRepository(Of EmployeeTask, Long)
            Get
                Return DirectCast(ReadOnlyRepository, IRepository(Of EmployeeTask, Long))
            End Get
        End Property

        Protected Overridable ReadOnly Property OpenFileDialogService() As IOpenFileDialogService
            Get
                Return Nothing
            End Get
        End Property
        Public Sub ShowPrintPreview()
            Dim printModel = Me.GetRequiredService(Of IPrintableControlPreviewService)().GetPreview()
        End Sub


        Private entityPanelViewModel_Renamed As EmployeeTaskDetailViewModel
        Public ReadOnly Property EntityPanelViewModel() As EmployeeTaskDetailViewModel
            Get
                If entityPanelViewModel_Renamed Is Nothing Then
                    entityPanelViewModel_Renamed = EmployeeTaskDetailViewModel.Create()
                End If
                Return entityPanelViewModel_Renamed
            End Get
        End Property
        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            SelectedEntities = New ObservableCollection(Of EmployeeTask)()
            FollowUpSelector = New ObservableCollection(Of Boolean)() From {False, False, False, False, False, False}
            PrioritySelector = New ObservableCollection(Of Boolean)() From {False, False}
        End Sub
        Public Sub OnLoaded()
            If FilterTreeViewModel Is Nothing Then
                Return
            End If
            FilterTreeViewModel.AllowFiltersContextMenu = False
            RefreshEmployeeFilters()
        End Sub
        Public Sub ChangeTaskView(ByVal parameter As String)

            Dim viewKind_Renamed As TasksViewKind = Nothing
            System.Enum.TryParse(parameter, viewKind_Renamed)
            If viewKind_Renamed = TasksViewKind.Active Then
                FilterTreeViewModel.SelectedItem = FilterTreeViewModel.StaticFilters(2)
            End If
            If viewKind_Renamed <> ViewKind AndAlso ViewKind = TasksViewKind.Active AndAlso FilterTreeViewModel.SelectedItem Is FilterTreeViewModel.StaticFilters(2) Then
                FilterTreeViewModel.SelectedItem = FilterTreeViewModel.StaticFilters(0)
            End If
            ViewKind = viewKind_Renamed
            EntityPanelViewModel.ZoomEditEnabled = (ViewKind = TasksViewKind.Detailed)
        End Sub
        Public Overrides Sub [New]()
            GetDocumentManagerService().ShowNewEntityDocument(Of EmployeeTask)(Me)
            Refresh()
            UpdateEntityPanelViewModel()
            RefreshEmployeeFilters()
        End Sub
        Public Overrides Sub Edit(ByVal entity As EmployeeTask)
            MyBase.Edit(entity)
            Refresh()
            UpdateEntityPanelViewModel()
            RefreshEmployeeFilters()
        End Sub
        Public Overrides Function CanEdit(ByVal projectionEntity As EmployeeTask) As Boolean
            Return MyBase.CanEdit(projectionEntity) AndAlso (SelectedEntities.Count = 1)
        End Function

        Public Overrides Sub Delete(ByVal projectionEntity As EmployeeTask)
            If MessageBoxService.ShowMessage(String.Concat("Do you want to delete ",If(SelectedEntities.Count = 1, "this EmployeeTask?", "selected EmployeeTasks?")), CommonResources.Confirmation_Caption, MessageButton.YesNo) <> MessageResult.Yes Then
                Return
            End If
            Try
                Do
                    Dim task As EmployeeTask = SelectedEntities.First()
                    Dim filesId() As Long = GetAttachedFilesId(task)
                    Entities.Remove(task)
                    Dim primaryKey As Long = Repository.GetProjectionPrimaryKey(task)
                    Dim entity As EmployeeTask = Repository.Find(primaryKey)
                    If entity IsNot Nothing Then
                        OnBeforeEntityDeleted(primaryKey, entity)
                        Repository.Remove(entity)
                        Repository.UnitOfWork.SaveChanges()
                        OnEntityDeleted(primaryKey, entity)
                    End If
                    DeleteAttachedFiles(filesId)
                Loop While SelectedEntities.Count > 0
            Catch e As DbException
                Refresh()
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error)
            End Try
            RefreshEmployeeFilters()
        End Sub
        Public Overrides Function CanDelete(ByVal projectionEntity As EmployeeTask) As Boolean
            Return (Not IsLoading) AndAlso SelectedEntities IsNot Nothing AndAlso SelectedEntities.Count > 0
        End Function
        Public Overridable Sub MarkComplete()
            For Each item As EmployeeTask In SelectedEntities
                If item.Status <> EmployeeTaskStatus.Completed Then
                    item.Status = EmployeeTaskStatus.Completed
                    item.Completion = 100
                    Save(item)
                    If item Is SelectedEntity Then
                        Me.UpdateSelectedEntity()
                    End If
                End If
            Next item
        End Sub
        Public Overridable Function CanMarkComplete() As Boolean
            Return (Not IsLoading) AndAlso SelectedEntities IsNot Nothing AndAlso SelectedEntities.Count > 0 AndAlso SelectedEntities.Where(Function(x) x.Status <> EmployeeTaskStatus.Completed).Count() > 0
        End Function
        Public Overridable Sub AssignTask(ByVal entity As EmployeeTask)
            Dim primaryKey As Long = Repository.GetProjectionPrimaryKey(entity)
            Dim document As IDocument = AssignTaskService.CreateDocument("EmployeeAssignView", primaryKey, Me)
            If document IsNot Nothing Then
                document.Show()
            End If
            Refresh()
            RefreshEmployeeFilters()
        End Sub
        Public Overridable Sub AttachFile(ByVal entity As EmployeeTask)
            Dim dialogResult As Boolean = OpenFileDialogService.ShowDialog()
            If dialogResult Then
                Dim file As IFileInfo = OpenFileDialogService.Files.First()
                If file.Length > FilesHelper.MaxAttachedFileSize * 1050578 Then
                    MessageBoxService.ShowMessage(String.Format("The file size exceeds {0} MB.", FilesHelper.MaxAttachedFileSize), "Error attaching file")
                    Return
                End If
                Dim unitOfWork As IDevAVDbUnitOfWork = Me.CreateUnitOfWork()
                Try
                    Using stream As New FileStream(Path.Combine(file.DirectoryName, file.Name), FileMode.Open, FileAccess.Read)
                        Dim attachedFile As TaskAttachedFile = unitOfWork.TaskAttachedFiles.Create()
                        attachedFile.EmployeeTaskId = entity.Id
                        attachedFile.Name = file.Name
                        attachedFile.Content = New Byte(CInt(stream.Length) - 1){}
                        stream.Read(attachedFile.Content, 0, CInt(stream.Length))
                        unitOfWork.SaveChanges()
                        Refresh()
                    End Using
                Catch e1 As Exception
                    MessageBoxService.ShowMessage("Error attaching file!", "Error")
                    Return
                End Try
            End If
        End Sub

        Public Overridable Function CanAttachFile(ByVal entity As EmployeeTask) As Boolean
            Return (Not IsLoading) AndAlso SelectedEntities IsNot Nothing AndAlso SelectedEntities.Count = 1
        End Function
        Public Overridable Property FollowUpSelector() As ObservableCollection(Of Boolean)
        Public Overridable Property PrioritySelector() As ObservableCollection(Of Boolean)
        Public Overridable Property [Private]() As Boolean?
        Public Overridable Property Category() As String

        Public Overridable Property GridControlFilterString() As String

        Private tagsFilterString As String

        Private Property CurrentFollowUp() As EmployeeTaskFollowUp?
        Private Property CurrentPriority() As EmployeeTaskPriority?

        Public Overridable Sub OnPrivateChanged()
            SetFilterString()
        End Sub
        Public Overridable Sub OnCategoryChanged()
            SetFilterString()
        End Sub

        Public Overridable Sub FollowUp(ByVal parameter As String)
            CurrentFollowUp = If(FollowUpSelector.Contains(True), DirectCast(System.Enum.Parse(GetType(EmployeeTaskFollowUp), parameter), EmployeeTaskFollowUp?), Nothing)
            SetFilterString()
        End Sub
        Public Overridable Sub Priority(ByVal parameter As String)
            CurrentPriority = If(PrioritySelector.Contains(True), DirectCast(System.Enum.Parse(GetType(EmployeeTaskPriority), parameter), EmployeeTaskPriority?), Nothing)
            SetFilterString()
        End Sub

        Private tagsFilterSetter As Boolean

        Public Overridable Sub OnGridControlFilterStringChanged()
            If tagsFilterSetter Then
                tagsFilterSetter = False
                Return
            End If
            For i As Integer = 0 To FollowUpSelector.Count - 1
                FollowUpSelector(i) = False
            Next i
            For i As Integer = 0 To PrioritySelector.Count - 1
                PrioritySelector(i) = False
            Next i
            CurrentFollowUp = Nothing
            CurrentPriority = Nothing
            [Private] = Nothing
            Category = Colors.Transparent.ToString()
        End Sub

        Private Sub SetFilterString()
            Dim filterStrings As New List(Of String)()
            If CurrentFollowUp IsNot Nothing Then
                filterStrings.Add("[FollowUp] == '" & CurrentFollowUp.ToString() & "'")
            End If
            If CurrentPriority IsNot Nothing Then
                filterStrings.Add("[Priority] == '" & CurrentPriority.ToString() & "'")
            End If
            If [Private] IsNot Nothing Then
                filterStrings.Add("[Private] == '" & [Private].ToString() & "'")
            End If
            If Category <> Colors.Transparent.ToString() AndAlso Category IsNot Nothing Then
                filterStrings.Add("[Category] == '" & Category.ToString() & "'")
            End If
            tagsFilterString = If(filterStrings.Count = 0, String.Empty, filterStrings(0))
            For i As Integer = 1 To filterStrings.Count() - 1
                tagsFilterString &= " and "
                tagsFilterString &= filterStrings(i)
            Next i
            tagsFilterSetter = True
            GridControlFilterString = tagsFilterString
        End Sub
        Private Function GetAttachedFilesId(ByVal task As EmployeeTask) As Long()
            Dim filesId() As Long = { }
            If task.AttachedFiles IsNot Nothing Then
                filesId = task.AttachedFiles.Select(Function(x) x.Id).ToArray()
            End If
            Return filesId
        End Function
        Private Sub DeleteAttachedFiles(ByVal filesId() As Long)
            If filesId Is Nothing Then
                Return
            End If
            Dim unitOfWork = Me.CreateUnitOfWork()
            For Each key In filesId
                Dim file = unitOfWork.TaskAttachedFiles.Find(key)
                unitOfWork.TaskAttachedFiles.Remove(file)
            Next key
            If filesId.Count() > 0 Then
                unitOfWork.SaveChanges()
            End If
        End Sub
        Private Sub RefreshEmployeeFilters()
            Dim getEmployeesFunction As Func(Of IDevAVDbUnitOfWork, IRepository(Of Employee, Long)) = Function(x) x.Employees
            Dim unitOfWork = unitOfWorkFactory.CreateUnitOfWork()
            Dim newFilters As New List(Of FilterInfo)()
            For Each employee In getEmployeesFunction(unitOfWork)
                newFilters.Add(New FilterInfo() With {.Name = employee.FullName, .FilterCriteria = TasksToFilterCriteriaConverter(employee.AssignedEmployeeTasks)})
            Next employee
            Dim oldFilters As IEnumerable(Of FilterInfo) = FilterTreeViewModel.CustomFilters.Select(Function(x) New FilterInfo() With {.Name = x.Name, .FilterCriteria = If(ReferenceEquals(x.FilterCriteria, Nothing), String.Empty, x.FilterCriteria.ToString())})
            If Not newFilters.SequenceEqual(oldFilters, New FilterInfoComparer()) Then
                If newFilters.Count <> oldFilters.Count() OrElse (Not newFilters.SequenceEqual(oldFilters, New FilterInfoNameComparer())) Then
                    FilterTreeViewModel.ResetToAll()
                    FilterTreeViewModel.ResetCustomFilters()
                    For Each newFilter In newFilters
                        FilterTreeViewModel.AddCustomFilter(newFilter.Name, CriteriaOperator.Parse(newFilter.FilterCriteria))
                    Next newFilter
                Else
                    Dim index As Integer = 0
                    For Each newFilter In newFilters
                        FilterTreeViewModel.ModifyCustomFilterCriteria(FilterTreeViewModel.CustomFilters.ElementAt(index), CriteriaOperator.Parse(newFilter.FilterCriteria))
                        index += 1
                    Next newFilter
                End If
            End If
        End Sub
        Public Sub PrintTasksSummary()
            ShowReport(ReportInfoFactory.TaskListReport(CreateUnitOfWork().Tasks.ToList()))
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
        Public ReadOnly Property AssignTaskService() As IDocumentManagerService
            Get
                Return Me.GetService(Of IDocumentManagerService)("EmployeeAssignService")
            End Get
        End Property

        Public Overridable Function CanAssignTask(ByVal entity As EmployeeTask) As Boolean
            Return (Not IsLoading) AndAlso SelectedEntities IsNot Nothing AndAlso SelectedEntities.Count = 1
        End Function
        Public Overridable Property FilterTreeViewModel() As FilterTreeViewModel(Of EmployeeTask, Long) Implements IFilterTreeViewModelContainer(Of EmployeeTask, Long).FilterTreeViewModel
        #Region "ISupportFiltering"
        Private Property ISupportFilteringGeneric_FilterExpression() As Expression(Of Func(Of EmployeeTask, Boolean)) Implements ISupportFiltering(Of EmployeeTask).FilterExpression
            Get
                Return FilterExpression
            End Get
            Set(ByVal value As Expression(Of Func(Of EmployeeTask, Boolean)))
                FilterExpression = value
            End Set
        End Property
        #End Region
        Public Overridable Property ViewKind() As TasksViewKind
        Public Overridable Property SelectedEntities() As ObservableCollection(Of EmployeeTask)

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            UpdateEntityPanelViewModel()
        End Sub
        Public Overrides Sub UpdateSelectedEntity()
            MyBase.UpdateSelectedEntity()
            UpdateEntityPanelViewModel()
        End Sub
        Private Sub UpdateEntityPanelViewModel()
            If SelectedEntity IsNot Nothing Then
                EntityPanelViewModel.Entity = Nothing
                EntityPanelViewModel.Entity = SelectedEntity
            End If
        End Sub
        Public Shared Function TasksToFilterCriteriaConverter(ByVal tasks As List(Of EmployeeTask)) As String
            Dim criteria As String = "[Id] = '-1'"
            If tasks IsNot Nothing AndAlso tasks.Count > 0 Then
                criteria = String.Empty
                For Each task In tasks
                    criteria &= "[Id] = '" & task.Id & "'" & (If(task IsNot tasks.ElementAt(tasks.Count - 1), " Or ", ""))
                Next task
            End If
            Return criteria
        End Function
        Private Sub ShowReport(ByVal reportInfo As IReportInfo)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
        End Sub
    End Class
    Friend Class FilterInfoComparer
        Implements IEqualityComparer(Of FilterInfo)

        Public Overloads Function Equals(ByVal info1 As FilterInfo, ByVal info2 As FilterInfo) As Boolean Implements IEqualityComparer(Of FilterInfo).Equals
            Return info1.Name = info2.Name AndAlso info1.FilterCriteria = info2.FilterCriteria
        End Function
        Public Overloads Function GetHashCode(ByVal info As FilterInfo) As Integer Implements IEqualityComparer(Of FilterInfo).GetHashCode
            Return info.ToString().ToLower().GetHashCode()
        End Function
    End Class
    Friend Class FilterInfoNameComparer
        Implements IEqualityComparer(Of FilterInfo)

        Public Overloads Function Equals(ByVal info1 As FilterInfo, ByVal info2 As FilterInfo) As Boolean Implements IEqualityComparer(Of FilterInfo).Equals
            Return info1.Name = info2.Name
        End Function
        Public Overloads Function GetHashCode(ByVal info As FilterInfo) As Integer Implements IEqualityComparer(Of FilterInfo).GetHashCode
            Return info.ToString().ToLower().GetHashCode()
        End Function
    End Class

    Public Enum TasksViewKind
        SimpleList
        Detailed
        Prioritized
        Active
    End Enum
End Namespace
