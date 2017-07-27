Imports System
Imports DevExpress.Xpf.Ribbon
Imports System.Windows

Namespace DevExpress.DevAV
    Partial Public Class MainWindow
        Inherits DXRibbonWindow

        Public Sub New()
            InitializeComponent()
            If Height > SystemParameters.VirtualScreenHeight OrElse Width > SystemParameters.VirtualScreenWidth Then
                WindowState = WindowState.Maximized
            End If
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, Me)
        End Sub
        Private Sub MainWindowLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Left < 0 OrElse Top < 0 Then
                WindowState = WindowState.Maximized
            End If
        End Sub
    End Class
End Namespace
