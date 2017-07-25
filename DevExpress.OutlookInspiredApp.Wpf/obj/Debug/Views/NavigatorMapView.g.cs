﻿#pragma checksum "..\..\..\Views\NavigatorMapView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "98E133E8E07D699DF78FAB3F742DBCF0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.DevAV;
using DevExpress.DevAV.ViewModels;
using DevExpress.DevAV.Views;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.ConditionalFormatting;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpf.Map;
using DevExpress.Xpf.Ribbon;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DevExpress.DevAV.Views {
    
    
    /// <summary>
    /// NavigatorMapView
    /// </summary>
    public partial class NavigatorMapView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Views\NavigatorMapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.DevAV.Views.NavigatorMapView root;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\NavigatorMapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Ribbon.RibbonControl ribbonControl;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Views\NavigatorMapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Map.MapControl mapControl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DevExpress.OutlookInspiredApp.Wpf;component/views/navigatormapview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\NavigatorMapView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.root = ((DevExpress.DevAV.Views.NavigatorMapView)(target));
            return;
            case 2:
            this.ribbonControl = ((DevExpress.Xpf.Ribbon.RibbonControl)(target));
            return;
            case 3:
            this.mapControl = ((DevExpress.Xpf.Map.MapControl)(target));
            return;
            case 4:
            
            #line 174 "..\..\..\Views\NavigatorMapView.xaml"
            ((DevExpress.Xpf.Map.BingGeocodeDataProvider)(target)).LayerItemsGenerating += new DevExpress.Xpf.Map.LayerItemsGeneratingEventHandler(this.BingGeocodeDataProvider_LayerItemsGenerating);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 182 "..\..\..\Views\NavigatorMapView.xaml"
            ((DevExpress.Xpf.Map.BingRouteDataProvider)(target)).LayerItemsGenerating += new DevExpress.Xpf.Map.LayerItemsGeneratingEventHandler(this.BingRouteDataProvider_LayerItemsGenerating);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 191 "..\..\..\Views\NavigatorMapView.xaml"
            ((DevExpress.Xpf.Map.BingSearchDataProvider)(target)).LayerItemsGenerating += new DevExpress.Xpf.Map.LayerItemsGeneratingEventHandler(this.BingGeocodeDataProvider_LayerItemsGenerating);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

