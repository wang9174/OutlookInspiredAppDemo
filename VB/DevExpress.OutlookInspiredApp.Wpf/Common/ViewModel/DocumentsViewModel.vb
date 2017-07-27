Imports System
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.Common.DataModel
Namespace DevExpress.DevAV.Common.ViewModel
  ''' <summary>
  ''' The base class for POCO view models that operate the collection of documents.
  ''' </summary>
  ''' <typeparam name="TModule">A navigation list entry type.</typeparam>
  ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
  Public MustInherit Class DocumentsViewModel(Of TModule As ModuleDescription(Of TModule), TUnitOfWork As IUnitOfWork)
    Private _IsLoaded As Boolean
    Private _Modules As TModule()
    Protected ReadOnly unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork)    
    ''' <summary>
    ''' Initializes a new instance of the DocumentsViewModel class.
    ''' </summary>
    ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork))
      Me.unitOfWorkFactory = unitOfWorkFactory
      _Modules = CreateModules().ToArray()
      For Each [module] In Modules
        Messenger.[Default].Register(Of NavigateMessage(Of TModule))(Me, [module], Sub(ByVal x) Show(x.Token))
      Next
    End Sub    
    ''' <summary>
    ''' Navigation list that represents a collection of module descriptions.
    ''' </summary>
    Public ReadOnly Property Modules As TModule()
      Get
        Return _Modules
      End Get
    End Property    
    ''' <summary>
    ''' A currently selected navigation list entry. This property is writable. When this property is assigned a new value, it triggers the navigating to the corresponding document.
    ''' Since DocumentsViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
    ''' </summary>
    Public Overridable Property SelectedModule As TModule    
    ''' <summary>
    ''' A navigation list entry that corresponds to the currently active document. If the active document does not have the corresponding entry in the navigation list, the property value is null. This property is read-only.
    ''' Since DocumentsViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
    ''' </summary>
    Public Overridable Property ActiveModule As TModule    
    ''' <summary>
    ''' Saves changes in all opened documents.
    ''' Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the SaveAllCommand property that can be used as a binding source in views.
    ''' </summary>
    Public Sub SaveAll()
      Messenger.[Default].Send(New SaveAllMessage())
    End Sub    
    ''' <summary>
    ''' Used to close all opened documents and allows you to save unsaved results and to cancel closing.
    ''' Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the OnClosingCommand property that can be used as a binding source in views.
    ''' </summary>
    ''' <param name="cancelEventArgs">An argument of the System.ComponentModel.CancelEventArgs type which is used to cancel closing if needed.</param>
    Public Sub OnClosing(ByVal cancelEventArgs As CancelEventArgs)
      Messenger.[Default].Send(New CloseAllMessage(cancelEventArgs))
    End Sub    
    ''' <summary>
    ''' Contains a current state of the navigation pane.
    ''' </summary>
    ''' Since DocumentsViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
    ''' 
    Public Overridable Property NavigationPaneVisibility As NavigationPaneVisibility    
    ''' <summary>
    ''' Navigates to a document.
    ''' Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the ShowCommand property that can be used as a binding source in views.
    ''' </summary>
    ''' <param name="module">A navigation list entry specifying a document what to be opened.</param>
    Public Sub Show(ByVal [module] As TModule)
      If [module] Is Nothing OrElse DocumentManagerService Is Nothing Then
        Return
      End If
      Dim document As IDocument = DocumentManagerService.FindDocumentByIdOrCreate([module], Function(ByVal x) CreateDocument([module]))
      document.Show()
    End Sub    
    ''' <summary>
    ''' Creates and shows a document which view is bound to PeekCollectionViewModel. The document is created and shown using a document manager service named "WorkspaceDocumentManagerService".
    ''' Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the PinPeekCollectionViewCommand property that can be used as a binding source in views.
    ''' </summary>
    ''' <param name="module">A navigation list entry that is used as a PeekCollectionViewModel factory.</param>
    Public Sub PinPeekCollectionView(ByVal [module] As TModule)
      If WorkspaceDocumentManagerService Is Nothing Then
        Return
      End If
      Dim document As IDocument = WorkspaceDocumentManagerService.FindDocumentByIdOrCreate([module], Function(ByVal x) CreatePinnedPeekCollectionDocument([module]))
      document.Show()
    End Sub    
    ''' <summary>
    ''' Finalizes the DocumentsViewModel initialization and opens the default document.
    ''' Since DocumentsViewModel is a POCO view model, an instance of this class will also expose the OnLoadedCommand property that can be used as a binding source in views.
    ''' </summary>
    Public Overridable Sub OnLoaded()
      _IsLoaded = True
      AddHandler DocumentManagerService.ActiveDocumentChanged, AddressOf OnActiveDocumentChanged
      Show(DefaultModule)
    End Sub
    Private Sub OnActiveDocumentChanged(ByVal sender As Object, ByVal e As ActiveDocumentChangedEventArgs)
      ActiveModule = If(e.NewDocument Is Nothing, Nothing, TryCast(e.NewDocument.Id, TModule))
    End Sub
    Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
      Get
        Return Me.GetService(Of IDocumentManagerService)("FrameDocumentManagerService")
      End Get
    End Property
    Protected ReadOnly Property WorkspaceDocumentManagerService As IDocumentManagerService
      Get
        Return Me.GetService(Of IDocumentManagerService)("WorkspaceDocumentManagerService")
      End Get
    End Property
    Protected Overridable ReadOnly Property DefaultModule As TModule
      Get
        Return Modules.First()
      End Get
    End Property
    Protected ReadOnly Property IsLoaded As Boolean
      Get
        Return _IsLoaded
      End Get
    End Property
    Protected Overridable Sub OnSelectedModuleChanged(ByVal oldModule As TModule)
      If IsLoaded Then
        Show(SelectedModule)
      End If
    End Sub
    Protected Overridable Sub OnActiveModuleChanged(ByVal oldModule As TModule)
      SelectedModule = ActiveModule
    End Sub
    Private Function CreateDocument(ByVal [module] As TModule) As IDocument
      Dim document = DocumentManagerService.CreateDocument([module].DocumentType, Nothing, Me)
      document.Title = GetModuleTitle([module])
      document.DestroyOnClose = False
      Return document
    End Function
    Protected Overridable Function GetModuleTitle(ByVal [module] As TModule) As String
      Return [module].ModuleTitle
    End Function
    Private Function CreatePinnedPeekCollectionDocument(ByVal [module] As TModule) As IDocument
      Dim document = WorkspaceDocumentManagerService.CreateDocument("PeekCollectionView", [module].CreatePeekCollectionViewModel())
      document.Title = [module].ModuleTitle
      Return document
    End Function
    Protected Function GetPeekCollectionViewModelFactory(Of TEntity As Class, TPrimaryKey)(ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey))) As Func(Of TModule, Object)
      Return Function(ByVal [module]) PeekCollectionViewModel(Of TModule, TEntity, TPrimaryKey, TUnitOfWork).Create([module], unitOfWorkFactory, getRepositoryFunc).SetParentViewModel(Me)
    End Function
    Protected MustOverride Function CreateModules() As TModule()
    Protected Function CreateUnitOfWork() As TUnitOfWork
      Return unitOfWorkFactory.CreateUnitOfWork()
    End Function
  End Class  
  ''' <summary>
  ''' A base class representing a navigation list entry.
  ''' </summary>
  ''' <typeparam name="TModule">A navigation list entry type.</typeparam>
  Public Partial MustInherit Class ModuleDescription(Of TModule As ModuleDescription(Of TModule))
    Private _DocumentType As String
    Private _ModuleGroup As String
    Private _ModuleTitle As String
    Private ReadOnly _peekCollectionViewModelFactory As Func(Of TModule, Object)
    Private _peekCollectionViewModel As Object    
    ''' <summary>
    ''' Initializes a new instance of the ModuleDescription class.
    ''' </summary>
    ''' <param name="title">A navigation list entry display text.</param>
    ''' <param name="documentType">A string value that specifies the view type of corresponding document.</param>
    ''' <param name="group">A navigation list entry group name.</param>
    ''' <param name="peekCollectionViewModelFactory">An optional parameter that provides a function used to create a PeekCollectionViewModel that provides quick navigation between collection views.</param>
    Public Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, Optional ByVal peekCollectionViewModelFactory As Func(Of TModule, Object) = Nothing)
      _ModuleTitle = title
      _ModuleGroup = group
      Me._DocumentType = documentType
      Me._peekCollectionViewModelFactory = peekCollectionViewModelFactory
    End Sub    
    ''' <summary>
    ''' The navigation list entry display text.
    ''' </summary>
    Public ReadOnly Property ModuleTitle As String
      Get
        Return _ModuleTitle
      End Get
    End Property    
    ''' <summary>
    ''' The navigation list entry group name.
    ''' </summary>
    Public ReadOnly Property ModuleGroup As String
      Get
        Return _ModuleGroup
      End Get
    End Property    
    ''' <summary>
    ''' Contains the corresponding document view type.
    ''' </summary>
    Public ReadOnly Property DocumentType As String
      Get
        Return _DocumentType
      End Get
    End Property    
    ''' <summary>
    ''' A primary instance of corresponding PeekCollectionViewModel used to quick navigation between collection views.
    ''' </summary>
    Public ReadOnly Property PeekCollectionViewModel As Object
      Get
        If _peekCollectionViewModelFactory Is Nothing Then
          Return Nothing
        End If
        If _peekCollectionViewModel Is Nothing Then
          _peekCollectionViewModel = CreatePeekCollectionViewModel()
        End If
        Return _peekCollectionViewModel
      End Get
    End Property    
    ''' <summary>
    ''' Creates and returns a new instance of the corresponding PeekCollectionViewModel that provides quick navigation between collection views.
    ''' </summary>
    Public Function CreatePeekCollectionViewModel() As Object
      Return _peekCollectionViewModelFactory(CType(Me, TModule))
    End Function
  End Class  
  ''' <summary>
  ''' Represents a navigation pane state.
  ''' </summary>
  Public Enum NavigationPaneVisibility  
    ''' <summary>
    ''' Navigation pane is visible and minimized.
    ''' </summary>
    Minimized    
    ''' <summary>
    ''' Navigation pane is visible and not minimized.
    ''' </summary>
    Normal    
    ''' <summary>
    ''' Navigation pane is invisible.
    ''' </summary>
    Off
  End Enum
End Namespace
