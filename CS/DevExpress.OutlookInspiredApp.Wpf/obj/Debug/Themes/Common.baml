   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5
      ^ ZDevExpress.OutlookInspiredApp.Wpf, Version=15.2.11.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DevExpress.DevAVDevExpress.DevAV 5      M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M3clr-namespace:System.ComponentModel;assembly=SystemSystem.ComponentModel 5      ?$clr-namespace:DevExpress.DevAV.ViewsDevExpress.DevAV.Views 5      I)clr-namespace:DevExpress.DevAV.ViewModelsDevExpress.DevAV.ViewModels 5      3clr-namespace:DevExpress.DevAVDevExpress.DevAV 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089b ^DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a^ ZDevExpress.Mvvm.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Printing.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ab
 ^DevExpress.Xpf.Grid.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49am iDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ak gDevExpress.Xpf.LayoutControl.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a  �� DataType$   DevExpress.Mvvm.EnumMemberInfo )  %OutlookInspiredEnumMemberInfoTemplate �� Orientation&   DevExpress.Xpf.Editors.ImageEdit%  DevExpress.Xpf.Editors.BaseEdit   
ShowBorder   
IsReadOnly   Source8  2DevExpress.DevAV.ViewModels.SortDirectionViewModel(  "DevExpress.Xpf.Editors.ListBoxEdit   ItemTemplate   ItemsSource,  &DevExpress.Xpf.Editors.EnumItemsSource   AllowImages   EnumType	   	EditValue5  /DevExpress.DevAV.ViewModels.SortByDateViewModel1  +DevExpress.DevAV.ViewModels.SortByViewModelD  >DevExpress.DevAV.ViewModels.SortDirectionAndDateRangeViewModel.	  (DevExpress.Xpf.LayoutControl.LayoutGroup
 	  Orientation%
  DevExpress.Xpf.Editors.DateEdit �� Mode=  7DevExpress.DevAV.ViewModels.SortByAndDateRangeViewModelG  ADevExpress.DevAV.ViewModels.EmployeeEvaluationsPrintModeViewModelD  >DevExpress.DevAV.ViewModels.CustomerContactsPrintModeViewModel6  0DevExpress.DevAV.ViewModels.SelectYearsViewModel)  #DevExpress.Xpf.Editors.ComboBoxEdit*  $DevExpress.Xpf.Editors.PopupBaseEdit   ShowSizeGrip'  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable+  %DevExpress.Xpf.Editors.LookUpEditBase   SeparatorString   ShowCustomItems   ItemsSource)  #DevExpress.Xpf.Editors.TextEditBase   Text �� UpdateSourceTrigger   StyleSettings9  3DevExpress.Xpf.Editors.CheckedComboBoxStyleSettingsA  ;DevExpress.DevAV.ViewModels.ProductImagesPrintModeViewModel0  *DevExpress.Mvvm.UI.ObjectToObjectConverter   personPrefixToImageConverter
   Map   DevExpress.Mvvm.UI.MapItem   Target   Source  fileViewTemplate � ToolTip � RowDefinitions�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      Bdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing 	  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 
   5      Fdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5	      .sys&clr-namespace:System;assembly=mscorlib  5
      (devAVclr-namespace:DevExpress.DevAV  5      FcomponentModel3clr-namespace:System.ComponentModel;assembly=System  5      -view$clr-namespace:DevExpress.DevAV.Views  5      7	viewModel)clr-namespace:DevExpress.DevAV.ViewModels  5      (localclr-namespace:DevExpress.DevAV  5      %&  &	        (�� �     5      *, +)7   (�� �    5      *, +)7   (��     5      *, +)7   (�� �    5       *, +)7   (�� �    5(      *, +)7   (�� F    50      *, +)7   (�� �    53      *, +)7   (�� �    56      *, +)(�� �    5?      *, +)7   &	 %    &	 �
    �� 5      "    6   .R�5   
   �� $ 
Horizontal=�6   .+�5         .  $��2q�6H    .  6   �� *Image+6S   �� 5      $
��12,0q�6.   $
��Bold"�6<   $��Center=�6N   ��6   �� *Name+6N   5      5      �� 5      "   6   .R�5   
     8    6`      .  $" viewModel:SortOrderPrintModeL�	 6�   �� *SortDirection+5      �� 5      "   6   .R�5   
     8    6`      .  $# viewModel:SortByDatePrintModeL�	 6�   �� *SortDirection+5      �� 5      "   6   .R�5   
     8    6`      .  $ viewModel:SortByPrintModeL�	 6�   �� *SortDirection+5      �� 5       "   6   .R�5!   
   	  $
 Vertical=�6   .+�5"        8    6d      .  $" viewModel:SortOrderPrintModeL�	 6�   �� *SortDirection+�� 5#      $��Range��6   $��22(�6%   
  5$      	 6   �� *
FromDate+$ TwoWay=�
  5%      	 6   �� *ToDate+$ TwoWay=�5&      5'      �� 5(      "   6   .R�5)   
   	  $
 Vertical=�6   .+�5*        8    6d      .  $ viewModel:SortByPrintModeL�	 6�   �� *SortDirection+�� 5+      $��Range��6   $��22(�6%   
  5,      	 6   �� *
FromDate+$ TwoWay=�
  5-      	 6   �� *ToDate+$ TwoWay=�5.      5/      �� 50      "   6   .R�51   
     8    6`      .  $, &viewModel:EmployeeEvaluationsPrintModeL�	 6�   �� *EmployeeEvaluationsPrintMode+52      �� 53      "   6   .R�54   
     8    6`      .  $) #viewModel:CustomerContactsPrintModeL�	 6�   �� *CustomerContactsPrintMode+55      �� 56      "   6   .R�57   
     $ FalseY�6   $ FalseX�60   $ ,��6G   $
 TrueX�6[    58      �� *AvailableYears+ 59      �� *Years+$ TwoWay=�$ PropertyChanged=� 5:        5;      5=      5>      �� 5?      "   6   .R�5@   
     8    6`      .  $&  viewModel:ProductImagesPrintModeL�	 6�   �� *ProductImagesPrintMode+5A        5B      . 5C   
     $] Wpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Doctor.png��6   $ Dr��6z     5D   
   $Y Spack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Mr.png��6   $ Mr��6v     5E   
   $Y Spack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Ms.png��6   $ Ms��6v     5F   
   $[ Upack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Miss.png��6   $
 Miss��6x     5G   
   $Z Tpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Mrs.png��6   $	 Mrs��6w   5H      �� 5I      .R�5J   
   � $��80��6    6   �� *Name+ 5K      �� 5L      $
��Auto �6    �� 5M      $
��Auto �6    .+�5O      �� $
��None=�6   ��6#   �� *Icon+�� 5P      $��1��6   $��0,5,0,0q�6B   $��Center=�6S   $��CharacterEllipsis=�6p   ��6%   �� *DisplayName+6p   5Q      5R      5U      