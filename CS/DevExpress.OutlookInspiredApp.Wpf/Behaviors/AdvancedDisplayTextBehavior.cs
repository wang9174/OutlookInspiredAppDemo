using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DevExpress.DevAV.Controls;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;

namespace DevExpress.DevAV {
    public class AdvancedDisplayTextBehavior : Behavior<AdvancedNavBarGroup> {
        public static readonly DependencyProperty FiltersProperty =
            DependencyProperty.Register("Filters", typeof(ObservableCollection<FilterItem>), typeof(AdvancedDisplayTextBehavior),
            new PropertyMetadata(null, (d, e) => ((AdvancedDisplayTextBehavior)d).OnFiltersChanged(e)));
        public ObservableCollection<FilterItem> Filters {
            get { return (ObservableCollection<FilterItem>)GetValue(FiltersProperty); }
            set { SetValue(FiltersProperty, value); }
        }
        public string[] DisplayProperties { get; set; }

        void OnFiltersChanged(DependencyPropertyChangedEventArgs e) {
            BindingOperations.ClearBinding(this.AssociatedObject, AdvancedNavBarGroup.AdvancedDisplayTextProperty);
            foreach(string property in DisplayProperties) {
                FilterItem item = Filters.FirstOrDefault(x => x.Name == property);
                if(item != null) {
                    BindingOperations.SetBinding(this.AssociatedObject, AdvancedNavBarGroup.AdvancedDisplayTextProperty, new Binding("EntitiesCount") { Source = item, Mode = BindingMode.OneWay });
                    return;
                }
            }
        }
        protected override void OnAttached() {
            base.OnAttached();
        }
        protected override void OnDetaching() {
            BindingOperations.ClearBinding(this.AssociatedObject, AdvancedNavBarGroup.AdvancedDisplayTextProperty);
            base.OnDetaching();
        }
    }
}
