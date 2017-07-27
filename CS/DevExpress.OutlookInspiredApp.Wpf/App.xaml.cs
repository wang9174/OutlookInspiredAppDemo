using System;
using System.Windows;
using System.Windows.Media.Animation;
using DevExpress.Images;
using System.Globalization;
using System.Threading;
using DevExpress.Mvvm.Native;
using DevExpress.Internal;
using DevExpress.Xpf.Core;
using System.Reflection;
using System.IO;
using DevExpress.Mvvm.UI;
using DevExpress.DevAV;
using DevExpress.Xpf.Printing;

namespace DevExpress.DevAV {
    public partial class App : Application {
        static IDisposable singleInstanceApplicationGuard;

        protected override void OnStartup(StartupEventArgs e) {
            Start(() => base.OnStartup(e), this);
        }
        public static void Start(Action baseStart, Application application) {
            ExceptionHelper.Initialize();
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            DataDirectoryHelper.LocalPrefix = "WpfOutlookInspiredApp";
            ImagesAssemblyLoader.Load();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            LoadPlugins();
            baseStart();
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            bool exit;
            singleInstanceApplicationGuard = DataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", out exit);
            if(exit) {
                application.Shutdown();
                return;
            }
            Theme.TouchlineDark.ShowInThemeSelector = false;
            ThemeManager.ApplicationThemeName = Theme.Office2013DarkGray.Name;
            SetCultureInfo();
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") {
                string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
        }
        #region LoadPlugins
        static void LoadPlugins() {
            foreach(string file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "DevExpress.OutlookInspiredApp.Wpf.Plugins.*.exe")) {
                Assembly.LoadFrom(file)
                    .With(x => x.GetType("Global.Program"))
                    .With(x => x.GetMethod("Start", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null))
                    .Do(x => x.Invoke(null, new object[] { }));
            }
        }
        #endregion
    }
}
#if CLICKONCE
    namespace DevExpress.Internal.DemoLauncher {
        public static class ClickOnceEntryPoint {
            public static Window CreateMainWindow() {
                Application app = Application.Current;
                app.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = DevExpress.Utils.AssemblyHelper.GetResourceUri(typeof(ClickOnceEntryPoint).Assembly, "Themes/Shared.xaml") });
                DevExpress.DevAV.App.Start(() => { }, app);
                return new DevExpress.DevAV.MainWindow();
            }
        }
    }
#endif
