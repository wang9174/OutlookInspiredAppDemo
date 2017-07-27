using System.Windows;
using DevExpress.Xpf.NavBar;

namespace DevExpress.DevAV.Controls {
    public class AdvancedNavBarGroup : NavBarGroup {
        public static readonly DependencyProperty AdvancedDisplayTextProperty =
            DependencyProperty.Register("AdvancedDisplayText", typeof(string), typeof(AdvancedNavBarGroup), new PropertyMetadata(null));
        public string AdvancedDisplayText {
            get { return (string)GetValue(AdvancedDisplayTextProperty); }
            set { SetValue(AdvancedDisplayTextProperty, value); }
        }
    }
}
