Imports System.Windows
Imports DevExpress.Xpf.Core

Namespace DevExpress.DevAV
    Partial Public Class SplashScreenWindow
        Inherits Window
        Implements ISplashScreen

        Public Sub New()
            InitializeComponent()
            Me.Visibility = If(System.Diagnostics.Debugger.IsAttached, Visibility.Hidden, Visibility.Visible)
        End Sub

        #Region "ISplashScreen"
        Private Sub ISplashScreen_Progress(ByVal value As Double) Implements ISplashScreen.Progress
            progressBar.Value = value
        End Sub
        Private Sub ISplashScreen_CloseSplashScreen() Implements ISplashScreen.CloseSplashScreen
            Me.Close()
        End Sub
        Private Sub ISplashScreen_SetProgressState(ByVal isIndeterminate As Boolean) Implements ISplashScreen.SetProgressState
        End Sub
        #End Region
    End Class
End Namespace
