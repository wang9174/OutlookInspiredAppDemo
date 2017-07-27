using System;
using DevExpress.Xpf.Ribbon;
using System.Windows;

namespace DevExpress.DevAV {
    public partial class MainWindow : DXRibbonWindow {
        public MainWindow() {
            InitializeComponent();
            if(Height > SystemParameters.VirtualScreenHeight || Width > SystemParameters.VirtualScreenWidth)
                WindowState = WindowState.Maximized;
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this);
        }
        void MainWindowLoaded(object sender, RoutedEventArgs e) {
            if(Left < 0 || Top < 0)
                WindowState = WindowState.Maximized;
        }
    }
}
