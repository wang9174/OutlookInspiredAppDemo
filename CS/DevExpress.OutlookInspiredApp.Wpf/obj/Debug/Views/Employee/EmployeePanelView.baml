   M S B A M L   `   `   `  ����^  ZDevExpress.OutlookInspiredApp.Wpf, Version=15.2.11.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:DevExpress.DevAV.ViewModelsDevExpress.DevAV.ViewModels  5      d `DevExpress.DevAV.v15.2.Data, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aXCclr-namespace:DevExpress.DevAV;assembly=DevExpress.DevAV.v15.2.DataDevExpress.DevAV 5      3clr-namespace:DevExpress.DevAVDevExpress.DevAV  5      .    (DevExpress.DevAV.Views.EmployeePanelView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/netfx/2009/xaml/presentation    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      b ^DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49am iDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag	 cDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5	      d
 `DevExpress.Xpf.Ribbon.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon 
 5
      k gDevExpress.Xpf.LayoutControl.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      ^ ZDevExpress.Mvvm.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      ;	viewmodel)clr-namespace:DevExpress.DevAV.ViewModels   5      QmodelCclr-namespace:DevExpress.DevAV;assembly=DevExpress.DevAV.v15.2.Data   5      f bDevExpress.Xpf.Controls.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxwui7http://schemas.devexpress.com/winfx/2008/xaml/windowsui  5      *localclr-namespace:DevExpress.DevAV  5      2  ,DevExpress.Mvvm.UI.Interactivity.Interaction    	Behaviors  5      -  'DevExpress.Xpf.Core.DXMessageBoxService  5   
   -   5          Name$ messageBoxService��5   !    � 	Resources 5       �� MergedDictionaries �� Source'   !DevExpress.DevAV.PictureConverter   PictureConverter0  *DevExpress.Mvvm.UI.ObjectToObjectConverter - )OrientationToSplitLineVisibilityConverter
   Map   DevExpress.Mvvm.UI.MapItem   Source   Target  EntityToOpacityConverter   DefaultTarget  EntityToVisibilityConverter#  DevExpress.Xpf.Grid.TableView	 �� 
TargetType
 �� Property �� Value& 	  DevExpress.Xpf.Grid.DataViewBase&   DevExpress.Xpf.Grid.GridViewBase  ShortBoldDate$	  DevExpress.Xpf.Grid.GridColumn$
 	 DevExpress.Xpf.Grid.ColumnBase �� Path �� 	Converter7  1DevExpress.Mvvm.UI.FormatStringConverterExtension   FormatString�� 5   
    5      �� 5      $[ Upack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Themes/Common.xaml?�6%   %!  &	        &	      &	 �     &	 �     '  U    &	       5      6%     5      . 5        $ Vertical��6!   $ Visible��63     5       $ 
Horizontal��6!   $ 	Collapsed��65   5!        5"      $ 1��6N   . 5#        $	 0.5��63    6!   Z� 63   5$        5%      $ 	Collapsed��6Q   . 5&        $ Visible��63    6!   Z� 63   5'      �� 5(      "	  6   .�5)      �� 
 �@ 	AutoWidth . 6.   �� 5*      
 �@ ShowIndicator .  62   �� 5+      
 �@ ShowVerticalLines .  66   �� 5,      
 �@ DetailHeaderContent$ Sales��68   �� 5-      
 �@ ShowGroupPanel .  63   �� 5.      
 �@ AllowEditing .  61   �� 5/      
 �@ AllowBestFit . 61   �� 50      
 �@ NavigationStyle$	 Row=�64   51      �� 52      "	 	 6*   .�53      �� 
 �@
 CellTemplate 54      �� 55      .R�56      �� $
��Bold"�6�   $	��4,2q�6�   ��6(   �� $ Value�   $ 
MM/dd/yyyy��6�   57      59      5:      5;      .��5=      � {�6   �� *Entity+ :  4DevExpress.Mvvm.UI.ObjectToBooleanConverterExtension   � RowDefinitions 5>   
   �� 5?      $
��Auto �6   �� 5@      $
��Auto �6   �� 5A      $��* �6    � ColumnDefinitions 5C   
   �� 5D      $
��Auto �6   �� 5E      $��* �6   .+�5G   
   � $	��130��6   $	��130��6   $��0,0,8,0q�6(   .+�5H      &   DevExpress.Xpf.Editors.ImageEdit  %  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly .    Stretch$ Uniform=�6�      	EditValue 6.   �� *Entity.Picture+# [  6�   )  #DevExpress.Xpf.Core.BackgroundPanel  5I       J� Opacity$	 0.8Q�6!   y�6/   �� *Entity+# [ 5J      � 5K   
   $��1��6   $��0,22,0,0q�6    5L      �� 5M      $
��Auto �6    �� 5N      $
��Auto �6    .+�5P      �� $��6,0,0,0q�6i    6   �� *Entity+# [ .+�5Q      �� $��24(�6=   $��0,-5,0,0q�6K   ��6   �� *Entity.FullName+6K   �� 5R      $��20(�6:   $��0,5,0,0q�6H   ��6   �� *Entity.Title+6H   5S      �� 5T      $��1��6    �� Orientation$ 
Horizontal=�6&   $	��Top=�6?    5U      ,  &DevExpress.Xpf.WindowsUI.CommandButton��%l  '        �� 5V      "	  6   .�5W      �� 
 � ��$ 0q�64   �� 5X      
 �@ AllowGlyphTheming . 6>   �� 5Y      
 �@ GlyphStretch$
 None=�69   �� 5Z      
 �@ EllipseDiameter$ 30Q�6<   �� 5[      
 � ��$	 4,0q�63   �� 5\      
 � ��$ Center=�6G   �� 5]      
 �@ StrokeThickness$ 2Q�6<   5^      5_      .+�5`           Glyph$o ipack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Buttons/glyph-message-16.png��6I   ��6&   �� *MessageCommand+6I     5a      $m gpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Buttons/glyph-phone-16.png��6G   ��6&   �� *PhoneCommand+6G     5b      $m gpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Buttons/glyph-video-16.png��6K   ��6&   �� *VideoCallCommand+6K     5c      $l fpack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Buttons/glyph-mail-16.png��6H   ��6&   �� *MailToCommand+6H   5d      5e      �� 5f   
   $��1��6   $��2��6   ���	#FFC6C6C6$��1q�6J   $��1��6^   $��	0,10,0,10q�6i   �� 5g   
   $��2��6   $��2��6"   $��0q�66   $��0q�6B    5h      ��%U   '��       �� 5i      "	 ��6   .�5j      �� 
 � ��$ Right=�6<   5k      5l      .@�5m      �� $��EVALUATIONS��6   .��5n      %  DevExpress.Xpf.Grid.GridControl     BorderThickness$ 0q�6M   ) 	 #DevExpress.Xpf.Grid.DataControlBase   ItemsSource 6"   �� *Entity.Evaluations+   View 5o        5p         UseLightweightTemplates$
 NoneX�6(      DataRowTemplate 5q      �� 5r   "   .R�5s   &   �� $ Vertical=�61   .+�5t   *   -  'DevExpress.Xpf.Core.MeasurePixelSnapper  ._�5u   .   ��  ��Name$ defineRowPresenter��6?   ��6[   �� ��6o   �� *View.DefaultDataRowTemplate+5v   +   �� 5w   *   $	��5,0q�6Q   $
��Wrap=�6^   ��64   �� *Row.Details+6^   5x   '   5y   #   5{         Columns 5}      	  5~       
  	FieldName$ 	CreatedOn��6)   $ 	 DevExpress.Xpf.Grid.BaseColumn   Header)  #DevExpress.Xpf.Core.ObjectConverter$ 
CREATED ON 6?       Width$	  100��6S   !   
FixedWidth! . #��[ 6q   5      	  5�      $ Subject��6)   $ SUBJECT 6=   	  5�      $ 	CreatedBy��6)   $ MANAGER 6?   " 
  AllowColumnFiltering^ ZDevExpress.Data.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$" False 6v   !# 
  FilterEditorHeaderTemplate# 6P   Z� 6v   5�      5�      �� 5�      $��TASKS��6   .��5�        $ 0q�6e    6"   �� *Entity.AssignedEmployeeTasks+$ �� Mode$$ OneWay=� 5�        5�      $
 NoneX�6(    5�      �� 5�   "   .R�5�   &   �� $ Vertical=�61   .+�5�   *     ._�5�   .   �� $ defineRowPresenter��6?   ��6[   �� ��6o   �� *View.DefaultDataRowTemplate+5�   +   �� 5�   *   $	��5,0q�6U   $
��Wrap=�6b   ��64   �� *Row.Description+6b   5�   '   5�   #   5�       5�      	  5�      $ DueDate��6)   $ DUE DATE 6=   $	  100��6O   ! . #��[ 6m   	  5�      $ Subject��6)   $ SUBJECT 6=   5�      5�      5�      5�      5�      