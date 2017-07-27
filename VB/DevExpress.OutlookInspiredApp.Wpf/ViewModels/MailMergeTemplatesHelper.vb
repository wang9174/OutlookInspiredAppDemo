Imports DevExpress.Utils
Imports System.IO
Imports System.Collections.Generic

Namespace DevExpress.DevAV.ViewModels

    Public NotInheritable Class MailMergeTemplatesHelper

        Private Sub New()
        End Sub

        Private Shared templateNames() As String = { "Employee of the Month.rtf", "Employee Probation Notice.rtf", "Employee Service Excellence.rtf", "Employee Thank You Note.rtf", "Welcome to DevAV.rtf", "Sales Thank You.rtf"}
        Public Shared Function GetAllTemplates() As List(Of TemplateViewModel)
            Dim templates As New List(Of TemplateViewModel)()
            For Each name In templateNames
                Dim stream As Stream = GetTemplateStream(name)
                templates.Add(New TemplateViewModel() With {.Name = name.Replace(".rtf", "")})
            Next name
            Return templates
        End Function
        Public Shared Function GetTemplateStream(ByVal templateName As String) As Stream
            Return AssemblyHelper.GetEmbeddedResourceStream(GetType(MailMergeTemplatesHelper).Assembly, templateName, False)
        End Function
    End Class
    Public Class TemplateViewModel
        Public Property Name() As String
        Public ReadOnly Property Template() As Stream
            Get
                Return MailMergeTemplatesHelper.GetTemplateStream(Name & ".rtf")
            End Get
        End Property
    End Class
End Namespace
