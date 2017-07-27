Imports System.Collections.Generic
Imports System.Windows.Controls
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV.Views
    Partial Public Class TaskCollectionView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub EmployeeTasks_ShowFilterPopup(ByVal sender As Object, ByVal e As FilterPopupEventArgs)
            If e.Column.FieldName <> "Status" Then
                Return
            End If
            Dim statusFilterItems As New List(Of Object)() From {
                New CustomComboBoxItem() With {.DisplayValue = "(All)", .EditValue = New CustomComboBoxItem()},
                New CustomComboBoxItem() With {.DisplayValue = "Complete", .EditValue = CriteriaOperator.Parse("[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#")},
                New CustomComboBoxItem() With {.DisplayValue = "Incomplete", .EditValue = CriteriaOperator.Parse("Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#)")}
            }
            e.ComboBoxEdit.ItemsSource = statusFilterItems
        End Sub
    End Class
End Namespace
