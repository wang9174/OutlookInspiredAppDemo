Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.DevAV.Common.ViewModel

Namespace DevExpress.DevAV.Common.View
    Public Class WorkspaceService
        Inherits Decorator
        Implements IWorkspaceService

        Private Class WorkspaceServiceInternal
            Inherits ServiceBase
            Implements IWorkspaceService

            Private workspaceService As IWorkspaceService

            Public Sub New(ByVal workspace As IWorkspaceService)
                Me.workspaceService = workspace
            End Sub
            Public Function SaveWorkspace() As Workspace Implements IWorkspaceService.SaveWorkspace
                Return workspaceService.SaveWorkspace()
            End Function
            Public Sub RestoreWorkspace(ByVal workspace As Workspace) Implements IWorkspaceService.RestoreWorkspace
                workspaceService.RestoreWorkspace(workspace)
            End Sub
        End Class
        Public Shared ReadOnly WorkspaceRegionRegisterEvent As RoutedEvent = EventManager.RegisterRoutedEvent("WorkspaceRegionRegister", RoutingStrategy.Bubble, GetType(WorkspaceRegionEventHandler), GetType(WorkspaceService))
        Public Shared ReadOnly WorkspaceRegionUnregisterEvent As RoutedEvent = EventManager.RegisterRoutedEvent("WorkspaceRegionUnregister", RoutingStrategy.Bubble, GetType(WorkspaceRegionEventHandler), GetType(WorkspaceService))

        Private regions As New Dictionary(Of String, IWorkspaceRegion)()
        Private workspace As New Workspace()
        Private workspaceChanging As Boolean = False

        Public Sub New()
            [AddHandler](WorkspaceRegionRegisterEvent, New WorkspaceRegionEventHandler(AddressOf OnWorkspaceRegionRegister))
            [AddHandler](WorkspaceRegionUnregisterEvent, New WorkspaceRegionEventHandler(AddressOf OnWorkspaceRegionUnregister))
            Interaction.GetBehaviors(Me).Add(New WorkspaceServiceInternal(Me))
        End Sub
        Private Sub OnWorkspaceRegionRegister(ByVal sender As Object, ByVal e As WorkspaceRegionEventArgs)
            e.Handled = True
            If Not regions.ContainsKey(e.Region.Id) Then
                regions.Add(e.Region.Id, e.Region)
            End If
            If Not workspaceChanging Then
                SyncWorkspaceRegionLayout(e.Region)
            End If
        End Sub
        Private Sub OnWorkspaceRegionUnregister(ByVal sender As Object, ByVal e As WorkspaceRegionEventArgs)
            e.Handled = True
            regions.Remove(e.Region.Id)
        End Sub
        Public Function SaveWorkspace() As Workspace Implements IWorkspaceService.SaveWorkspace
            If workspaceChanging Then
                Throw New InvalidOperationException()
            End If
            workspaceChanging = True
            workspace = New Workspace()
            For Each region As IWorkspaceRegion In regions.Values
                workspace.AddRegion(region.Id, region.SaveLayout())
            Next region
            Return workspace
        End Function
        Public Sub RestoreWorkspace(ByVal workspace As Workspace) Implements IWorkspaceService.RestoreWorkspace
            If Not workspaceChanging Then
                Throw New InvalidOperationException()
            End If
            Me.workspace = workspace
            For Each region As IWorkspaceRegion In regions.Values
                SyncWorkspaceRegionLayout(region)
            Next region
            workspaceChanging = False
        End Sub
        Private Sub SyncWorkspaceRegionLayout(ByVal region As IWorkspaceRegion)
            Dim regionLayout As String = workspace.FindRegionLayout(region.Id)
            If regionLayout IsNot Nothing Then
                region.RestoreLayout(regionLayout)
            Else
                workspace.AddRegion(region.Id, region.SaveLayout())
            End If
        End Sub
    End Class
    Public Interface IWorkspaceRegion
        ReadOnly Property Id() As String
        Function SaveLayout() As String
        Sub RestoreLayout(ByVal layout As String)
    End Interface
    Public Class WorkspaceRegionEventArgs
        Inherits RoutedEventArgs

        Public Sub New(ByVal region As IWorkspaceRegion)
            Me.Region = region
        End Sub
        Private privateRegion As IWorkspaceRegion
        Public Property Region() As IWorkspaceRegion
            Get
                Return privateRegion
            End Get
            Private Set(ByVal value As IWorkspaceRegion)
                privateRegion = value
            End Set
        End Property
    End Class
    Public Delegate Sub WorkspaceRegionEventHandler(ByVal sender As Object, ByVal e As WorkspaceRegionEventArgs)
    Public Class WorkspaceRegionBehavior
        Inherits Behavior(Of FrameworkElement)
        Implements IWorkspaceRegion

        #Region "Dependency Properties"
        Public Shared ReadOnly IdProperty As DependencyProperty = DependencyProperty.Register("Id", GetType(String), GetType(WorkspaceRegionBehavior), New PropertyMetadata(Nothing))
        #End Region

        Private restoreLayoutFromStreamMethod_Renamed As MethodInfo

        Private saveLayoutToStreamMethod_Renamed As MethodInfo

        Public Property Id() As String
            Get
                Return CStr(GetValue(IdProperty))
            End Get
            Set(ByVal value As String)
                SetValue(IdProperty, value)
            End Set
        End Property
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoaded
            AddHandler AssociatedObject.Unloaded, AddressOf OnAssociatedObjectUnloaded
            If AssociatedObject.IsLoaded Then
                OnAssociatedObjectLoaded(AssociatedObject, Nothing)
            End If
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoaded
            RemoveHandler AssociatedObject.Unloaded, AddressOf OnAssociatedObjectUnloaded
            If AssociatedObject.IsLoaded Then
                OnAssociatedObjectUnloaded(AssociatedObject, Nothing)
            End If
        End Sub
        Private Sub OnAssociatedObjectUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AssociatedObject.RaiseEvent(New WorkspaceRegionEventArgs(Me) With {.RoutedEvent = WorkspaceService.WorkspaceRegionUnregisterEvent})
        End Sub
        Private Sub OnAssociatedObjectLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AssociatedObject.RaiseEvent(New WorkspaceRegionEventArgs(Me) With {.RoutedEvent = WorkspaceService.WorkspaceRegionRegisterEvent})
        End Sub
        Private Function GetMethod(ByVal type As Type, ByVal methodName As String) As MethodInfo
            Do While type IsNot Nothing
                Dim method As MethodInfo = type.GetMethod(methodName)
                If method IsNot Nothing Then
                    Return method
                End If
                type = type.BaseType
            Loop
            Throw New InvalidOperationException()
        End Function
        Private ReadOnly Property RestoreLayoutFromStreamMethod() As MethodInfo
            Get
                If restoreLayoutFromStreamMethod_Renamed Is Nothing Then
                    restoreLayoutFromStreamMethod_Renamed = GetMethod(AssociatedObject.GetType(), "RestoreLayoutFromStream")
                End If
                Return restoreLayoutFromStreamMethod_Renamed
            End Get
        End Property
        Private ReadOnly Property SaveLayoutToStreamMethod() As MethodInfo
            Get
                If saveLayoutToStreamMethod_Renamed Is Nothing Then
                    saveLayoutToStreamMethod_Renamed = GetMethod(AssociatedObject.GetType(), "SaveLayoutToStream")
                End If
                Return saveLayoutToStreamMethod_Renamed
            End Get
        End Property
        Private Sub SaveLayoutToStream(ByVal stream As Stream)
            SaveLayoutToStreamMethod.Invoke(AssociatedObject, New Object() { stream })
        End Sub
        Private Sub RestoreLayoutFromStream(ByVal stream As Stream)
            RestoreLayoutFromStreamMethod.Invoke(AssociatedObject, New Object() { stream })
        End Sub
        Private ReadOnly Property IWorkspaceRegion_Id() As String Implements IWorkspaceRegion.Id
            Get
                Return Id
            End Get
        End Property
        Private Function IWorkspaceRegion_SaveLayout() As String Implements IWorkspaceRegion.SaveLayout
            Using stream As New MemoryStream()
                SaveLayoutToStream(stream)
                Return Encoding.UTF8.GetString(stream.ToArray())
            End Using
        End Function
        Private Sub IWorkspaceRegion_RestoreLayout(ByVal layout As String) Implements IWorkspaceRegion.RestoreLayout
            Using stream As New MemoryStream(Encoding.UTF8.GetBytes(layout))
                RestoreLayoutFromStream(stream)
            End Using
        End Sub
    End Class
End Namespace
