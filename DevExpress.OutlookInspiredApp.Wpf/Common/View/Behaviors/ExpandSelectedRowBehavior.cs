using System;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;

namespace DevExpress.DevAV.Common.View {
    public class ExpandSelectedRowBehavior : Behavior<GridControl> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.Loaded += OnAssociatedObjectLoaded;
            if(AssociatedObject.IsLoaded)
                ExpandSelectedRow();
        }
        void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e) {
            ExpandSelectedRow();
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
        }
        void ExpandSelectedRow() {
            int[] selectedRowHandles = AssociatedObject.GetSelectedRowHandles();
            if(selectedRowHandles != null && selectedRowHandles.Length > 0) {
                int handle = selectedRowHandles.First();
                AssociatedObject.ExpandMasterRow(handle);
            }
        }
    }
}
