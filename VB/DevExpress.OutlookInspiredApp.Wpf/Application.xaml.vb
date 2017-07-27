Imports System
Imports System.Windows
Imports System.Windows.Media.Animation
Imports DevExpress.Images
Imports System.Globalization
Imports System.Threading
Imports DevExpress.Mvvm.Native
Imports DevExpress.Internal
Imports DevExpress.Xpf.Core
Imports System.Reflection
Imports System.IO
Imports DevExpress.Mvvm.UI
Imports DevExpress.DevAV
Imports DevExpress.Xpf.Printing

Namespace DevExpress.DevAV
    Partial Public Class App
        Inherits Application

        Private Shared singleInstanceApplicationGuard As IDisposable

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Start(Sub() MyBase.OnStartup(e), Me)
        End Sub
        Public Shared Sub Start(ByVal baseStart As Action, ByVal application As Application)
            ExceptionHelper.Initialize()
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnCurrentDomainAssemblyResolve
            DataDirectoryHelper.LocalPrefix = "WpfOutlookInspiredApp"
            ImagesAssemblyLoader.Load()
            Timeline.DesiredFrameRateProperty.OverrideMetadata(GetType(Timeline), New FrameworkPropertyMetadata(200))
            LoadPlugins()
            baseStart()
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            Dim [exit] As Boolean = Nothing
            singleInstanceApplicationGuard = DataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", [exit])
            If [exit] Then
                application.Shutdown()
                Return
            End If
            Theme.TouchlineDark.ShowInThemeSelector = False
            ThemeManager.ApplicationThemeName = Theme.Office2013DarkGray.Name
            SetCultureInfo()
        End Sub
        Private Shared Sub SetCultureInfo()
            Dim demoCI As CultureInfo = DirectCast(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
            demoCI.NumberFormat.CurrencySymbol = "$"
            Thread.CurrentThread.CurrentCulture = demoCI
            Dim demoUI As CultureInfo = DirectCast(Thread.CurrentThread.CurrentUICulture.Clone(), CultureInfo)
            demoUI.NumberFormat.CurrencySymbol = "$"
            Thread.CurrentThread.CurrentUICulture = demoUI
        End Sub
        Private Shared Function OnCurrentDomainAssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As System.Reflection.Assembly
            Dim partialName As String = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower()
            If partialName = "entityframework" OrElse partialName = "system.data.sqlite" OrElse partialName = "system.data.sqlite.ef6" Then
                Dim path As String = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(GetType(App).Assembly.Location), partialName & ".dll")
                Return System.Reflection.Assembly.LoadFrom(path)
            End If
            Return Nothing
        End Function
        #Region "LoadPlugins"
        Private Shared Sub LoadPlugins()
            For Each file As String In Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "DevExpress.OutlookInspiredApp.Wpf.Plugins.*.exe")
                System.Reflection.Assembly.LoadFrom(file).With(Function(x) x.GetType("Global.Program")).With(Function(x) x.GetMethod("Start", BindingFlags.Static Or BindingFlags.Public, Nothing, New Type() { }, Nothing)).Do(Function(x) x.Invoke(Nothing, New Object() { }))
            Next file
        End Sub
        #End Region
    End Class
End Namespace
#If CLICKONCE Then
    Namespace DevExpress.Internal.DemoLauncher
        Public NotInheritable Class ClickOnceEntryPoint

            Private Sub New()
            End Sub

            Public Shared Function CreateMainWindow() As Window

                Dim app_Renamed As Application = Application.Current
                app_Renamed.Resources.MergedDictionaries.Add(New ResourceDictionary() With {.Source = DevExpress.Utils.AssemblyHelper.GetResourceUri(GetType(ClickOnceEntryPoint).Assembly, "Themes/Shared.xaml")})
                DevExpress.DevAV.App.Start(Sub()
                End Sub, app)
                Return New DevExpress.DevAV.MainWindow()
            End Function
        End Class
    End Namespace
#End If
