   M S B A M L   `   `   `  ����^  ZDevExpress.OutlookInspiredApp.Wpf, Version=15.2.11.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DevExpress.DevAVDevExpress.DevAV  5	   	   -    'DevExpress.DevAV.Views.CustomFilterView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   b ^DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	   ^ ZDevExpress.Mvvm.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5   	   ,devavclr-namespace:DevExpress.DevAV   5	   	   .��5      ]� $��
12,12,12,0q�6   .+�5   
   %  DevExpress.Xpf.Editors.TextEdit  %  DevExpress.Xpf.Editors.BaseEdit    NullText$,  &Enter a name for your custom filter...��6   $��Bottom=�6I      	EditValue 6a   �� *
FilterName+�� 5   
   $��Bottom=�6   $
��Left=�6-   $
��0,10q�6H   .+�5      &   DevExpress.Xpf.Editors.CheckEdit     Content)  #DevExpress.Xpf.Core.ObjectConverter$ Save for future use 6    6:   �� *Save+5      4  .DevExpress.Xpf.Editors.Filtering.FilterControl  5   
   -   5          Name$ FilterControl��5      $	��500��6j   $	��200��6y      FilterCriteria 63   �� *FilterCriteria+ �� Mode$ TwoWay=�2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors 5      '  !DevExpress.Mvvm.UI.EventToCommand  5      �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ IsVisibleChanged��6(   +	  %DevExpress.Mvvm.UI.EventToCommandBase 	  Command 6E   �� 	 �� ElementName$	 FilterControl��
 �� Path$
 ApplyFilterCommand�C
  =DevExpress.Xpf.Editors.Filtering.FilterControlColumnsBehavior
  5       
  UpperCasePropertyNames .  
  
ObjectType 63   �� *
EntityType+ 
  HiddenProperties 6s   �� *HiddenProperties+ 
  AdditionalProperties 6�   �� *AdditionalProperties+ �� 	Converter 5  /DevExpress.Mvvm.UI.EnumerableConverterExtension  *5  /DevExpress.Mvvm.UI.ReflectionConverterExtension  +5      5      5      