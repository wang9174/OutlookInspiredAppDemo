Imports System
Imports System.Diagnostics
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class EmployeeContactsViewModel
        Public Shared Function Create() As EmployeeContactsViewModel
            Return ViewModelSource.Create(Function() New EmployeeContactsViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Entity() As Employee
        Protected ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Me.GetRequiredService(Of IMessageBoxService)()
            End Get
        End Property

        Public Sub Message()
            MessageBoxService.Show("Send an IM to: " & Entity.Skype)
        End Sub
        Public Function CanMessage() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Skype)
        End Function
        Public Sub Phone()
            MessageBoxService.Show("Phone Call: " & Entity.MobilePhone)
        End Sub
        Public Function CanPhone() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.MobilePhone)
        End Function
        Public Sub HomeCall()
            MessageBoxService.Show("Home Call: " & Entity.HomePhone)
        End Sub
        Public Function CanHomeCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.HomePhone)
        End Function
        Public Sub MobileCall()
            MessageBoxService.Show("Mobile Call: " & Entity.MobilePhone)
        End Sub
        Public Function CanMobileCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.MobilePhone)
        End Function
        Public Sub [Call]()
            MessageBoxService.Show("Call: " & Entity.Skype)
        End Sub
        Public Function CanCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Skype)
        End Function
        Public Sub VideoCall()
            MessageBoxService.Show("Video Call: " & Entity.Skype)
        End Sub
        Public Function CanVideoCall() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Skype)
        End Function
        Public Sub MailTo()
            ExecuteMailTo(MessageBoxService, Entity.Email)
        End Sub
        Public Function CanMailTo() As Boolean
            Return Entity IsNot Nothing AndAlso Not String.IsNullOrEmpty(Entity.Email)
        End Function
        Protected Overridable Sub OnEntityChanged()
            Me.RaiseCanExecuteChanged(Sub(x) x.Message())
            Me.RaiseCanExecuteChanged(Sub(x) x.Phone())
            Me.RaiseCanExecuteChanged(Sub(x) x.MobileCall())
            Me.RaiseCanExecuteChanged(Sub(x) x.HomeCall())
            Me.RaiseCanExecuteChanged(Sub(x) x.Call())
            Me.RaiseCanExecuteChanged(Sub(x) x.VideoCall())
            Me.RaiseCanExecuteChanged(Sub(x) x.MailTo())
        End Sub
        Public Shared Sub ExecuteMailTo(ByVal messageBoxService As IMessageBoxService, ByVal email As String)
            Try
                Process.Start("mailto://" & email)
            Catch
                If messageBoxService IsNot Nothing Then
                    messageBoxService.Show("Mail To: " & email)
                End If
            End Try
        End Sub
    End Class
End Namespace
