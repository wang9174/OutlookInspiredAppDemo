   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      ^ ZDevExpress.OutlookInspiredApp.Wpf, Version=15.2.11.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:DevExpress.DevAV.ViewsDevExpress.DevAV.Views 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35b ^DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ad `DevExpress.Xpf.Ribbon.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a^ ZDevExpress.Mvvm.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089)   #DevExpress.Xpf.Ribbon.RibbonControl  �� 
TargetType �� Property �� Value"   System.Collections.ArrayList5  /DevExpress.Xpf.Ribbon.RibbonSpacingSelectorItem!  DevExpress.Xpf.Bars.BarItem   Content)  #DevExpress.Xpf.Core.ObjectConverter   	MergeType5  /DevExpress.DevAV.Views.ObjectsEqualityConverter   objectsEqualityConverter # objectsEqualityInverseConverter   Inverse0  *DevExpress.Mvvm.UI.ObjectToObjectConverter  RibbonStyleToGlyphConverter
   Map   DevExpress.Mvvm.UI.MapItem   Source   Target4  .DevExpress.DevAV.Views.RibbonStyleSelectorItem	 �� RelativeSource,	  &DevExpress.Xpf.Bars.BarSplitButtonItem
 �� 	Converter(
  "DevExpress.Xpf.Bars.GalleryControl 
  Gallery!  DevExpress.Xpf.Bars.Gallery   AllowFilter)   "ItemDescriptionHorizontalAlignment   AutoHideGallery   ItemCheckMode   Groups*  $DevExpress.Xpf.Bars.GalleryItemGroup   Caption   Items%  DevExpress.Xpf.Bars.GalleryItem   Caption   Description   Glyph   	IsChecked �� Mode �� ConverterParameter  waitIndicator'  !DevExpress.Xpf.Core.WaitIndicator   DeferedVisibility  silentWaitIndicator � Triggers :� RoutedEvent ��Name �� 
TargetName �� TargetProperty W� From	  W� To! g� Duration  BitmapButton" �� 
TargetType# � 	Resources  Key$   DefaultTarget% �� Orientation& �� Path  ObjectToVisibilityConverter�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      >dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      :sys2clr-namespace:System.Collections;assembly=mscorlib  5      -view$clr-namespace:DevExpress.DevAV.Views  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %�  '         &	  �     &	     '��     &	 �    '  '    7  7   7   7   7   &	 H    &	 |    &	 �    7  �� 5
      "    6   .�5   
   ��  �@  ToolbarShowMode$ 	ShowAbove=�6,   �� 5   
    �@  ToolbarItemLinksSource 5        5        5      $ Spacing mode 64   $ Replace=�6K   5      5      5        5      6$     5       . 6L   �� 5      "  ��6   .�5   
   ��  � ��$ Stretch=�67   �� 5   
    � ��$ Stretch=�65   5        5      . 5   
     $ 
Office2007��6   $s mpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/Office2007_16x16.png��6-     5   
   $ 
Office2010��6   $s mpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/Office2010_16x16.png��6-     5   
   $ 
OfficeSlim��6   $s mpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/OfficeSlim_16x16.png��6-     5   
   $ TabletOffice��6   $u opack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/TabletOffice_16x16.png��6/   5      �� 5       "   6   .�5!   
   ��  �@ RibbonStyle$ SmallWithText=�6(   �� 5"   
    �@ Content 6$   �� *SelectedRibbonStyle+	 �� *Self+�� 5#   
    �@	 ActAsDropDown . 6*   �� 5$   
    �@ Glyph 6"   �� *SelectedRibbonStyle+	 �� *Self+8
   �� 5%   
    �@ 	MergeType$ Replace=�6&   �� 5&   
    �@ PopupTemplate 5'      �� 5(      .R�5)      
  $��galleryControl��6)   . 5*         .  $
 Left=�6:    . $ Single=�6{   . 5+        $ Classic 63   . 5,   "     $ 
Office2007 62   $! MS Office 2007 style Ribbon 6G   $s mpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/Office2007_32x32.png��6   6q   �� *Selector.SelectedRibbonStyle+$ TwoWay=�8
  $ 
Office2007��6    5-   "   $ 
Office2010 62   $! MS Office 2010 style Ribbon 6G   $s mpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/Office2010_32x32.png��6   6q   �� *Selector.SelectedRibbonStyle+$ TwoWay=�8
  $ 
Office2010��6  5.        5/      $ Tablet 63   . 50   "     $ TabletOffice 62   $1 +Emulates the UI found in MS Office for iPad 6I   $u opack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/TabletOffice_32x32.png��6   6�   �� *Selector.SelectedRibbonStyle+$ TwoWay=�8
  $ TabletOffice��6    51   "   $ 
OfficeSlim 62   $$ MS Office 2014 inspired Ribbon 6G   $s mpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/RibbonStyle/OfficeSlim_32x32.png��6   6t   �� *Selector.SelectedRibbonStyle+$ TwoWay=�8
  $ 
OfficeSlim��6  52      53      54      55      57      58      �� 59      .R�5:   
      . 6   5;      �� 5<      .R�5=   
     $��waitIndicator��64    .  5>      :� 5?      $ FrameworkElement.Unloaded��6   .[�5@      �� .��5A      �� $ board��6%   .��5B      W� $ waitIndicator��6.   $ Opacity�6T   $	 1.0X�6x   $  0X�6�   $! 0:0:1.5D�6�   5C      5D      5E      5G      5H      �� 5I      "  ��6   .�5J   
   ��  � �� 5K      �� 5L      "" ��6"   # 5M      ��  5N      ObjectToVisibilityConverter $$ Visible��6]   . 5O        $ 	Collapsed��6?    6-   Z� 6?   5P      5Q      .R�5R      �� $% Vertical=�6!   .+�5S      �� $
��None=�6    $��Center=�6{   ��6/   �� *	Content+	 �� *TemplatedParent+6{   �� 5T      $��Center=�6�   ��6$   �� $!& (AutomationProperties.Name)�	 �� *TemplatedParent+y�5U   $   �� $!& (AutomationProperties.Name)�	 �� *TemplatedParent+8
   5V      5W      5Y      5Z      5[      