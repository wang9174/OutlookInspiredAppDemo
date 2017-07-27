Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.DevAV
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels
    Public Class CustomerAnalysisViewModel
        Implements IDocumentContent

        Private unitOfWork As IDevAVDbUnitOfWork

        Public Shared Function Create() As CustomerAnalysisViewModel
            Return ViewModelSource.Create(Function() New CustomerAnalysisViewModel())
        End Function
        Protected Sub New()
            unitOfWork = UnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork()
        End Sub
        Public Function GetSalesReport() As IEnumerable(Of CustomersAnalysis.Item)
            Return CustomersAnalysis.GetSalesReport(unitOfWork)
        End Function
        Public Function GetSalesData() As IEnumerable(Of CustomersAnalysis.Item)
            Return CustomersAnalysis.GetSalesData(unitOfWork)
        End Function
        Public Function GetStates(ByVal states As IEnumerable(Of StateEnum)) As IEnumerable(Of String)
            Return QueriesHelper.GetStateNames(unitOfWork.States, states)
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
