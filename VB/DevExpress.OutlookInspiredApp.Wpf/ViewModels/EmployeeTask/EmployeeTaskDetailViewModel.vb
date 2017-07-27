Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO


Namespace DevExpress.DevAV.ViewModels
    Public Class EmployeeTaskDetailViewModel
        Public Shared Function Create() As EmployeeTaskDetailViewModel
            Return ViewModelSource.Create(Function() New EmployeeTaskDetailViewModel())
        End Function

        Public Overridable Property FilesInfo() As ObservableCollection(Of AttachedFileInfo)
        Public Property SelectedFile() As AttachedFileInfo
        Private createdFilePaths As List(Of String)

        Protected Sub New()
        End Sub
        Public Overridable Property Entity() As EmployeeTask
        Protected ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Me.GetRequiredService(Of IMessageBoxService)()
            End Get
        End Property
        Public Overridable Property ZoomEditEnabled() As Boolean

        Public Overridable Sub OnEntityChanged()
            Me.RaisePropertyChanged(Function(x) x.FollowUp)
            If Entity Is Nothing Then
                Return
            End If
            FilesInfo = New ObservableCollection(Of AttachedFileInfo)()
            For Each file In Entity.AttachedFiles
                FilesInfo.Add(FilesHelper.GetAttachedFileInfo(file.Name))
            Next file
        End Sub

        Public Overridable ReadOnly Property FollowUp() As String
            Get
                Return If(Entity Is Nothing, String.Empty, "Follow Up. " & GetStartDate() & GetDueDate() & GetReminderDateTime() & (If(Entity.FollowUp <> EmployeeTaskFollowUp.NoDate, String.Format("Due {0}.", GetFollowUp(Entity.FollowUp)), String.Empty)))
            End Get
        End Property
        Public Overridable Sub Unloaded()
            FilesHelper.DeleteTempFiles(createdFilePaths)
        End Sub
        Public Overridable Sub OpenFile()
            If createdFilePaths Is Nothing Then
                createdFilePaths = New List(Of String)()
            End If
            Dim index As Integer = FilesInfo.IndexOf(SelectedFile)
            If Entity.AttachedFilesCount > index Then
                createdFilePaths.Add(FilesHelper.OpenFileFromDb(SelectedFile.Name, Entity.AttachedFiles(index).Content))
            End If
        End Sub
        Public Overridable Function CanOpenFile() As Boolean
            Return SelectedFile IsNot Nothing
        End Function
        Private Function GetStartDate() As String
            If Entity.StartDate Is Nothing Then
                Return String.Empty
            End If
            Return String.Format("Start by {0:dddd, MMMM d, yyyy}. ", Entity.StartDate.Value)
        End Function
        Private Function GetDueDate() As String
            If Entity.DueDate Is Nothing Then
                Return String.Empty
            End If
            Return String.Format("Due by {0:dddd, MMMM d, yyyy}. ", Entity.DueDate.Value)
        End Function
        Private Function GetReminderDateTime() As String
            If (Not Entity.Reminder) OrElse Entity.ReminderDateTime Is Nothing Then
                Return String.Empty
            End If
            Return String.Format("Reminder by {0:dddd, MMMM d, yyyy hh:mm tt}. ", Entity.ReminderDateTime.Value)
        End Function
        Private Function GetFollowUp(ByVal followUp As EmployeeTaskFollowUp) As String
            Dim fieldInfo = followUp.GetType().GetField(followUp.ToString())
            Return CType(fieldInfo.GetCustomAttributes(GetType(DisplayAttribute), False).FirstOrDefault(), DisplayAttribute).Name
        End Function

    End Class
End Namespace
