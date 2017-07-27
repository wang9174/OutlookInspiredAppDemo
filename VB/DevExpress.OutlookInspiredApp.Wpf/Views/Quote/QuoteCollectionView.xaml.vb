Imports DevExpress.Xpf.PivotGrid
Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views
    Partial Public Class QuoteCollectionView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub CustomSummary(ByVal sender As Object, ByVal e As PivotCustomSummaryEventArgs)
            Select Case e.DataField.FieldName
                Case "Percentage"
                    Dim quoteSummary As Decimal = 0
                    Dim opportunitySummary As Decimal = 0
                    For Each row As PivotDrillDownDataRow In e.CreateDrillDownDataSource()
                        quoteSummary += CDec(row("Total"))
                        opportunitySummary += CDec(row("MoneyOpportunity"))
                    Next row
                    If quoteSummary <> 0 Then
                        e.CustomValue = 100D * opportunitySummary / quoteSummary
                    End If
            End Select
        End Sub
        Private Sub pivotGridSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            Dim pivotGrid As PivotGridControl = DirectCast(sender, PivotGridControl)
            Dim fieldPercentage As PivotGridField = (pivotGrid.Fields).GetFieldByName("fieldPercentage")
            Dim fieldQuote As PivotGridField = (pivotGrid.Fields).GetFieldByName("fieldQuote")
            Dim estimatedWidth As Double = e.NewSize.Width - pivotGrid.RowTreeOffset - pivotGrid.RowTreeMinWidth - fieldQuote.MinWidth - 2
            If estimatedWidth > fieldPercentage.MinWidth Then
                fieldPercentage.Width = estimatedWidth
            End If
        End Sub
    End Class
End Namespace
