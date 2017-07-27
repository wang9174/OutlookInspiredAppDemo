Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.DevAV
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class ProductAnalysisViewModel
        Implements IDocumentContent

        Private unitOfWork As IDevAVDbUnitOfWork

        Public Shared Function Create() As ProductAnalysisViewModel
            Return ViewModelSource.Create(Function() New ProductAnalysisViewModel())
        End Function
        Protected Sub New()
            unitOfWork = UnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork()
        End Sub
        Public Function GetFinancialReport() As IEnumerable(Of ProductsAnalysis.Item)
            Return ProductsAnalysis.GetFinancialReport(unitOfWork)
        End Function
        Public Function GetFinancialData() As IEnumerable(Of ProductsAnalysis.Item)
            Return ProductsAnalysis.GetFinancialData(unitOfWork)
        End Function
        Public Sub Close()
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub
        Private privateDocumentOwner As IDocumentOwner
        Protected Property DocumentOwner() As IDocumentOwner
            Get
                Return privateDocumentOwner
            End Get
            Private Set(ByVal value As IDocumentOwner)
                privateDocumentOwner = value
            End Set
        End Property
        #Region "IDocumentContent"
        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub
        Private Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwner
            End Get
            Set(ByVal value As IDocumentOwner)
                DocumentOwner = value
            End Set
        End Property
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return "DevAV - Sales Analysis"
            End Get
        End Property
        #End Region
    End Class
End Namespace
