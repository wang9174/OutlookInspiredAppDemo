using System.Windows;
using System.Windows.Controls;
using DevExpress.DevAV.ViewModels;
using DevExpress.Xpf.Map;

namespace DevExpress.DevAV.Views {
    public partial class NavigatorMapView : UserControl {
        public FrameworkElement DetailsForm {
            get { return (FrameworkElement)GetValue(DetailsFormProperty); }
            set { SetValue(DetailsFormProperty, value); }
        }
        public static readonly DependencyProperty DetailsFormProperty =
            DependencyProperty.Register("DetailsForm", typeof(FrameworkElement), typeof(NavigatorMapView), new PropertyMetadata(null));

        public NavigatorMapView() {
            InitializeComponent();
        }
        private void BingGeocodeDataProvider_LayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            foreach(var pushpinItem in args.Items) {
                MapPushpin pushpin = pushpinItem as MapPushpin;
                if(pushpin != null) {
                    ((INavigatorMapViewModel)DataContext).NewPushpinCreated(pushpin);
                }
            }
        }
        private void BingRouteDataProvider_LayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            ((MapPushpin)args.Items[1]).Text = "A";
            ((MapPushpin)args.Items[2]).Text = "B";
            mapControl.ZoomToRegion(((MapPushpin)args.Items[1]).Location, ((MapPushpin)args.Items[2]).Location, 0.4);
        }
    }
}
