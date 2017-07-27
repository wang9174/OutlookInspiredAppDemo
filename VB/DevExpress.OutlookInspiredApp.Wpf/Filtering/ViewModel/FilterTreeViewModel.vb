Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Utils
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class FilterTreeViewModel(Of TEntity As Class, TPrimaryKey)
        Implements IFilterTreeViewModel

        Shared Sub New()
            Dim enums = GetType(EmployeeStatus).Assembly.GetTypes().Where(Function(t) t.IsEnum)
            For Each e As Type In enums
                EnumProcessingHelper.RegisterEnum(e)
            Next e
        End Sub

        Public Shared Function Create(ByVal settings As IFilterTreeModelPageSpecificSettings, ByVal entities As IQueryable(Of TEntity), ByVal registerEntityChangedMessageHandler As Action(Of Object, Action)) As FilterTreeViewModel(Of TEntity, TPrimaryKey)
            Return ViewModelSource.Create(Function() New FilterTreeViewModel(Of TEntity, TPrimaryKey)(settings, entities, registerEntityChangedMessageHandler))
        End Function

        Private ReadOnly entities As IQueryable(Of TEntity)
        Private ReadOnly settings As IFilterTreeModelPageSpecificSettings
        Private viewModel As Object

        Protected Sub New(ByVal settings As IFilterTreeModelPageSpecificSettings, ByVal entities As IQueryable(Of TEntity), ByVal registerEntityChangedMessageHandler As Action(Of Object, Action))
            Me.settings = settings
            Me.entities = entities
            StaticFilters = CreateFilterItems(settings.StaticFilters)
            CustomFilters = CreateFilterItems(settings.CustomFilters)
            SelectedItem = StaticFilters.FirstOrDefault()
            AllowFiltersContextMenu = True

            Messenger.Default.Register(Of EntityMessage(Of TEntity, TPrimaryKey))(Me, Sub(message) UpdateFilters())
            Messenger.Default.Register(Of CreateCustomFilterMessage(Of TEntity))(Me, Sub(message) CreateCustomFilter())
            UpdateFilters()
            StaticCategoryName = settings.StaticFiltersTitle
            CustomCategoryName = settings.CustomFiltersTitle
        End Sub

        Public Overridable Property StaticFilters() As ObservableCollection(Of FilterItem)
        Public Overridable Property CustomFilters() As ObservableCollection(Of FilterItem)
        Public Overridable Property SelectedItem() As FilterItem
        Public Overridable Property ActiveFilterItem() As FilterItem
        Public Property NavigateAction() As Action Implements IFilterTreeViewModel.NavigateAction
        Private privateStaticCategoryName As String
        Public Property StaticCategoryName() As String
            Get
                Return privateStaticCategoryName
            End Get
            Private Set(ByVal value As String)
                privateStaticCategoryName = value
            End Set
        End Property
        Private privateCustomCategoryName As String
        Public Property CustomCategoryName() As String
            Get
                Return privateCustomCategoryName
            End Get
            Private Set(ByVal value As String)
                privateCustomCategoryName = value
            End Set
        End Property
        Public ReadOnly Property AllowCustomFilters() As Boolean
            Get
                Return settings.CustomFilters IsNot Nothing
            End Get
        End Property
        Public Property AllowFiltersContextMenu() As Boolean
            Get
                Return AllowCustomFilters AndAlso allowFiltersContextMenu_Renamed
            End Get
            Set(ByVal value As Boolean)
                allowFiltersContextMenu_Renamed = value
            End Set
        End Property

        Private allowFiltersContextMenu_Renamed As Boolean


        Public Sub DeleteCustomFilter(ByVal filterItem_Renamed As FilterItem)
            CustomFilters.Remove(filterItem_Renamed)
            SaveCustomFilters()
        End Sub


        Public Sub DuplicateFilter(ByVal filterItem_Renamed As FilterItem)
            Dim newItem = filterItem_Renamed.Clone("Copy of " & filterItem_Renamed.Name, Nothing)
            CustomFilters.Add(newItem)
            SaveCustomFilters()
        End Sub

        Public Sub ResetCustomFilters()
            If CustomFilters.Contains(SelectedItem) Then
                SelectedItem = Nothing
            End If
            settings.CustomFilters = New FilterInfoList()
            CustomFilters.Clear()
            settings.SaveSettings()
        End Sub

        Public Sub ModifyCustomFilter(ByVal existing As FilterItem)
            Dim clone As FilterItem = existing.Clone()
            Dim filterViewModel = CreateCustomFilterViewModel(clone, True)
            ShowFilter(clone, filterViewModel, Sub()
                existing.FilterCriteria = clone.FilterCriteria
                existing.Name = clone.Name
                SaveCustomFilters()
                If existing Is SelectedItem Then
                    OnSelectedItemChanged()
                End If
            End Sub)
        End Sub
        Public Sub ModifyCustomFilterCriteria(ByVal existing As FilterItem, ByVal criteria As CriteriaOperator)
            If (Not ReferenceEquals(existing.FilterCriteria, Nothing)) AndAlso (Not ReferenceEquals(criteria, Nothing)) AndAlso existing.FilterCriteria.ToString() = criteria.ToString() Then
                Return
            End If
            existing.FilterCriteria = criteria
            SaveCustomFilters()
            If existing Is SelectedItem Then
                OnSelectedItemChanged()
            End If
            UpdateFilters()
        End Sub
        Public Sub ResetToAll()
            SelectedItem = StaticFilters(0)
        End Sub

        Public Sub CreateCustomFilter()

            Dim filterItem_Renamed As FilterItem = CreateFilterItem(String.Empty, Nothing, Nothing)
            Dim filterViewModel = CreateCustomFilterViewModel(filterItem_Renamed, True)
            ShowFilter(filterItem_Renamed, filterViewModel, Sub() AddNewCustomFilter(filterItem_Renamed))
        End Sub
        Public Sub AddCustomFilter(ByVal name As String, ByVal filterCriteria As CriteriaOperator, Optional ByVal imageUri As String = Nothing)
            AddNewCustomFilter(CreateFilterItem(name, filterCriteria, imageUri))
        End Sub
        Protected Overridable Sub OnSelectedItemChanged()
            ActiveFilterItem = SelectedItem.Clone()
            UpdateFilterExpression()
            NavigateCore()
        End Sub
        Public Overridable Sub Navigate()
            NavigateCore()
        End Sub
        Private Sub NavigateCore()
            If NavigateAction IsNot Nothing Then
                NavigateAction()()
            End If
        End Sub
        Private Sub IFilterTreeViewModel_SetViewModel(ByVal viewModel As Object) Implements IFilterTreeViewModel.SetViewModel
            Me.viewModel = viewModel
            Dim viewModelContainer = TryCast(viewModel, IFilterTreeViewModelContainer(Of TEntity, TPrimaryKey))
            If viewModelContainer IsNot Nothing Then
                viewModelContainer.FilterTreeViewModel = Me
            End If
            UpdateFilterExpression()
        End Sub

        Private Sub UpdateFilterExpression()
            Dim viewModel As ISupportFiltering(Of TEntity) = TryCast(Me.viewModel, ISupportFiltering(Of TEntity))
            If viewModel IsNot Nothing Then
                viewModel.FilterExpression = If(ActiveFilterItem Is Nothing, Nothing, GetWhereExpression(ActiveFilterItem.FilterCriteria))
            End If
        End Sub

        Private Function CreateFilterItems(ByVal filters As IEnumerable(Of FilterInfo)) As ObservableCollection(Of FilterItem)
            If filters Is Nothing Then
                Return New ObservableCollection(Of FilterItem)()
            End If
            Return New ObservableCollection(Of FilterItem)(filters.Select(Function(x) CreateFilterItem(x.Name, CriteriaOperator.Parse(x.FilterCriteria), x.ImageUri)))
        End Function

        Private Const NewFilterName As String = "New Filter"


        Private Sub AddNewCustomFilter(ByVal filterItem_Renamed As FilterItem)
            If String.IsNullOrEmpty(filterItem_Renamed.Name) Then
                Dim prevIndex As Integer = CustomFilters.Select(Function(fi) Regex.Match(fi.Name, NewFilterName & " (?<index>\d+)")).Where(Function(m) m.Success).Select(Function(m) Integer.Parse(m.Groups("index").Value)).DefaultIfEmpty(0).Max()
                filterItem_Renamed.Name = NewFilterName & " " & (prevIndex + 1)
            Else
                Dim existing = CustomFilters.FirstOrDefault(Function(fi) fi.Name = filterItem_Renamed.Name)
                If existing IsNot Nothing Then
                    CustomFilters.Remove(existing)
                End If
            End If
            CustomFilters.Add(filterItem_Renamed)
            SaveCustomFilters()
        End Sub

        Private Sub SaveCustomFilters()
            settings.CustomFilters = SaveToSettings(CustomFilters)
            settings.SaveSettings()
        End Sub

        Private Function SaveToSettings(ByVal filters As ObservableCollection(Of FilterItem)) As FilterInfoList
            Return New FilterInfoList(filters.Select(Function(fi) New FilterInfo With {.Name = fi.Name, .FilterCriteria = CriteriaOperator.ToString(fi.FilterCriteria)}))
        End Function

        Private Sub UpdateFilters()
            For Each item In StaticFilters.Concat(CustomFilters)
                item.Update(GetEntityCount(item.FilterCriteria))
            Next item
        End Sub


        Private Sub ShowFilter(ByVal filterItem_Renamed As FilterItem, ByVal filterViewModel As CustomFilterViewModel, ByVal onSave As Action)
            If FilterDialogService.ShowDialog(MessageButton.OKCancel, "Create Custom Filter", "CustomFilterView", filterViewModel) <> MessageResult.OK Then
                Return
            End If
            filterItem_Renamed.FilterCriteria = filterViewModel.FilterCriteria
            filterItem_Renamed.Name = filterViewModel.FilterName
            ActiveFilterItem = filterItem_Renamed
            If filterViewModel.Save Then
                onSave()
                UpdateFilters()
            End If
        End Sub

        Private Function CreateCustomFilterViewModel(ByVal existing As FilterItem, ByVal save As Boolean) As CustomFilterViewModel
            Dim viewModel = CustomFilterViewModel.Create(GetType(TEntity), settings.HiddenFilterProperties, settings.AdditionalFilterProperties)
            viewModel.FilterCriteria = existing.FilterCriteria
            viewModel.FilterName = existing.Name
            viewModel.Save = save
            viewModel.SetParentViewModel(Me)
            Return viewModel
        End Function

        Private Function CreateFilterItem(ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String) As FilterItem
            Return FilterItem.Create(GetEntityCount(filterCriteria), name, filterCriteria, imageUri)
        End Function

        Private Function GetEntityCount(ByVal criteria As CriteriaOperator) As Integer
            Return entities.Where(GetWhereExpression(criteria)).Count()
        End Function

        Private Function GetWhereExpression(ByVal criteria As CriteriaOperator) As Expression(Of Func(Of TEntity, Boolean))
            Return If(Me.IsInDesignMode(), CriteriaOperatorToExpressionConverter.GetLinqToObjectsWhere(Of TEntity)(criteria), CriteriaOperatorToExpressionConverter.GetGenericWhere(Of TEntity)(criteria))
        End Function

        Private ReadOnly Property FilterDialogService() As IDialogService
            Get
                Return Me.GetRequiredService(Of IDialogService)("FilterDialogService")
            End Get
        End Property
    End Class

    Public Interface IFilterTreeViewModelContainer(Of TEntity As Class, TPrimaryKey)
        Property FilterTreeViewModel() As FilterTreeViewModel(Of TEntity, TPrimaryKey)
    End Interface

    Public Class CreateCustomFilterMessage(Of TEntity As Class)
    End Class

    Public Interface IFilterTreeViewModel
        Sub SetViewModel(ByVal content As Object)
        Property NavigateAction() As Action
    End Interface
End Namespace
