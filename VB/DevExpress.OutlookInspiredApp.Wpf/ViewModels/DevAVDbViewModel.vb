Imports System
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Namespace DevExpress.DevAV.ViewModels
  ''' <summary>
  ''' Represents the root POCO view model for the DevAVDb data model.
  ''' </summary>
  Public Partial Class DevAVDbViewModel
    Inherits DocumentsViewModel(Of DevAVDbModuleDescription, IDevAVDbUnitOfWork)
    Private Const _TablesGroup As String = "Tables"    
    ''' <summary>
    ''' Creates a new instance of DevAVDbViewModel as a POCO view model.
    ''' </summary>
    Public Shared Function Create() As DevAVDbViewModel
      Return ViewModelSource.Create(Function() New DevAVDbViewModel())
    End Function    
    ''' <summary>
    ''' Initializes a new instance of the DevAVDbViewModel class.
    ''' This constructor is declared protected to avoid undesired instantiation of the DevAVDbViewModel type without the POCO proxy factory.
    ''' </summary>
    Protected Sub New()
      MyBase.New(UnitOfWorkSource.GetUnitOfWorkFactory())
      NavigationPaneVisibility = NavigationPaneVisibility.Normal
    End Sub
    Protected Overrides Function CreateModules() As DevAVDbModuleDescription()
      Return New DevAVDbModuleDescription() { New DevAVDbModuleDescription("Employees", "EmployeeCollectionView", _TablesGroup, FiltersSettings.GetEmployeesFilterTree(Me), GetPeekCollectionViewModelFactory(Function(ByVal x) x.Employees)),New DevAVDbModuleDescription("Tasks", "TaskCollectionView", _TablesGroup, FiltersSettings.GetTasksFilterTree(Me)),New DevAVDbModuleDescription("Customers", "CustomerCollectionView", _TablesGroup, FiltersSettings.GetCustomersFilterTree(Me), GetPeekCollectionViewModelFactory(Function(ByVal x) x.Customers)),New DevAVDbModuleDescription("Products", "ProductCollectionView", _TablesGroup, FiltersSettings.GetProductsFilterTree(Me), GetPeekCollectionViewModelFactory(Function(ByVal x) x.Products)),New DevAVDbModuleDescription("Sales", "OrderCollectionView", _TablesGroup, FiltersSettings.GetSalesFilterTree(Me)),New DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", _TablesGroup, FiltersSettings.GetOpportunitiesFilterTree(Me)) }
    End Function
    Private _defaultWorkspace As Workspace = New Workspace()
    Private _linksViewModel As LinksViewModel
    Public Overrides Sub OnLoaded()
      MyBase.OnLoaded()
      RegisterJumpList()
      Dim timer = New DispatcherTimer()
      timer.Interval = New TimeSpan(0, 0, 0, 90)
      AddHandler timer.Tick, AddressOf OnTimerTick
      timer.Start()
    End Sub
    Protected Overrides Sub OnActiveModuleChanged(ByVal oldModule As DevAVDbModuleDescription)
      MyBase.OnActiveModuleChanged(oldModule)
      If ActiveModule IsNot Nothing AndAlso ActiveModule.FilterTreeViewModel IsNot Nothing Then
        ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content)
      End If
      MainWindowService.Title = Convert.ToString(DocumentManagerService.ActiveDocument.Title) + " - DevAV"
      FinishModuleChangingDispatcherService.BeginInvoke(Sub()
        UpdateWorkspace(oldModule, ActiveModule)
      End Sub)
    End Sub
    Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
      NotificationService.CreatePredefinedNotification("DevAV Tips & Tricks", "Take users where they want to go with DevExpress Map Controls.", "").ShowAsync()
      CType(sender, DispatcherTimer).[Stop]()
    End Sub
    Private ReadOnly Property SignleObjectDocumentManagerService As IDocumentManagerService
      Get
        Return Me.GetService(Of IDocumentManagerService)("SignleObjectDocumentManagerService")
      End Get
    End Property
    Private ReadOnly Property WorkspaceService As IWorkspaceService
      Get
        Return Me.GetRequiredService(Of IWorkspaceService)()
      End Get
    End Property
    Private ReadOnly Property MainWindowService As IMainWindowService
      Get
        Return Me.GetRequiredService(Of IMainWindowService)()
      End Get
    End Property
    Private ReadOnly Property FinishModuleChangingDispatcherService As IDispatcherService
      Get
        Return Me.GetRequiredService(Of IDispatcherService)("FinishModuleChangingDispatcherService")
      End Get
    End Property
    Private ReadOnly Property NotificationService As INotificationService
      Get
        Return Me.GetRequiredService(Of INotificationService)()
      End Get
    End Property
    Private Sub UpdateWorkspace(ByVal oldModule As DevAVDbModuleDescription, ByVal newModule As DevAVDbModuleDescription)
      Dim oldWorkspace As Workspace = WorkspaceService.SaveWorkspace()
      Dim newWorkspace As Workspace = New Workspace()
      Try
        If oldModule IsNot Nothing Then
          oldModule.Workspace = oldWorkspace
        End If
        If newModule IsNot Nothing Then
          newWorkspace = If(newModule.Workspace, _defaultWorkspace)
        End If
      Finally
        WorkspaceService.RestoreWorkspace(newWorkspace)
      End Try
    End Sub
    Public ReadOnly Property LinksViewModel As LinksViewModel
      Get
        If _linksViewModel Is Nothing Then
          _linksViewModel = LinksViewModel.Create()
        End If
        Return _linksViewModel
      End Get
    End Property
    Private Sub RegisterJumpList()
      Dim jumpListService As IApplicationJumpListService = Me.GetService(Of IApplicationJumpListService)()
      jumpListService.Items.AddOrReplace("Modules", "New Employee", EmployeeIcon, Sub() SignleObjectDocumentManagerService.ShowNewEntityDocument(Of Employee)(Me))
      jumpListService.Items.AddOrReplace("Modules", "Customers", CustomerIcon, Sub() Show(Modules.Where(Function(ByVal m) m.DocumentType = "CustomerCollectionView").First()))
      jumpListService.Items.AddOrReplace("Modules", "Opportunities", OpportunitiesIcon, Sub() Show(Modules.Where(Function(ByVal m) m.DocumentType = "QuoteCollectionView").First()))
      jumpListService.Apply()
    End Sub
    Private ReadOnly Property EmployeeIcon As ImageSource
      Get
        Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-new-employee-16.png"))
      End Get
    End Property
    Private ReadOnly Property CustomerIcon As ImageSource
      Get
        Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-new-customers-16.png"))
      End Get
    End Property
    Private ReadOnly Property OpportunitiesIcon As ImageSource
      Get
        Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/icon-new-opportunities-16.png"))
      End Get
    End Property
  End Class
  Public Partial Class DevAVDbModuleDescription
    Inherits ModuleDescription(Of DevAVDbModuleDescription)
    Private _FilterTreeViewModel As IFilterTreeViewModel
    Private _ImageSource As Uri
    Public Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, ByVal filterTreeViewModel As IFilterTreeViewModel, Optional ByVal peekCollectionViewModelFactory As Func(Of DevAVDbModuleDescription, Object) = Nothing)
      MyBase.New(title, documentType, group, peekCollectionViewModelFactory)
      _ImageSource = New Uri(String.Format("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Modules/icon-nav-{0}-32.png", title.ToLowerInvariant()))
      Me._FilterTreeViewModel = filterTreeViewModel
    End Sub
    Public Property Workspace As Workspace
    Public ReadOnly Property ImageSource As Uri
      Get
        Return _ImageSource
      End Get
    End Property
    Public ReadOnly Property FilterTreeViewModel As IFilterTreeViewModel
      Get
        Return _FilterTreeViewModel
      End Get
    End Property
  End Class
End Namespace
