Imports DevExpress.Data.Filtering
Imports DevExpress.DevAV
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Xpf.Editors.Filtering
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace DevExpress.DevAV.Views
    Partial Public Class CustomFilterView
        Inherits UserControl

        Private ReadOnly Property ViewModel() As CustomFilterViewModel
            Get
                Return DirectCast(DataContext, CustomFilterViewModel)
            End Get
        End Property
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
