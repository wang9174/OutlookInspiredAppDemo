   M S B A M L   `   `   `  ����%  !DevExpress.OutlookInspiredApp.Wpf)    #DevExpress.DevAV.SplashScreenWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Jdxet?http://schemas.devexpress.com/winfx/2008/xaml/editors/themekeys  5      -   5          �Name$  splashWindow��5	       � WindowStyle$
 None=�5
       � 
ResizeMode$ NoResize=�6   $	��800��6.   $	��600��6:    � AllowsTransparency . 
������  � ShowInTaskbar .   � Topmost .  � 	Resources 5      I  CDevExpress.Xpf.Editors.Themes.ProgressBarEditStyleThemeKeyExtension�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   ResourceKey�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionBase`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   IsThemeIndependent	 �� 
TargetTypeD  >DevExpress.Xpf.Editors.Themes.ProgressBarEditThemeKeyExtension�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Editors.Themes.ProgressBarEditThemeKeys, DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]
   ResourceKey�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionBase`1[[DevExpress.Xpf.Editors.Themes.ProgressBarEditThemeKeys, DevExpress.Xpf.Core.v15.2, Version=15.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   IsThemeIndependent ��Name � UseLayoutRounding ��Name��%g  (        5      $: 4ProgressBarMarqueeHorizontalAnimationElementTemplate�� . )(  >     5      $'
 !PanelBackgroundHorizontalTemplate=� . )�� 5   
   .R�5      �� ���	#FFF7941E6   5      �� 5   
   "	 ��6�   .R�5      �� $ Border��6   
������� . ._�5      �� $ content��6#   $��1q�64   5      5      5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5   
   � -   5        $ Splash��5      $	��498��6   $	��368��6,   $��Center=�69   $	��Top=�6V   $��	0,100,0,0q�6n   .+�5      � -   5        $
 Back��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 1Q�6'      	Direction$	 -90Q�67      
BlurRadius$ 10Q�6G      Opacity$
 0.25Q�6W   .+�5      �� 
���   � �� CornerRadius$ 3��6,    J� Opacity$
 0.15Q�6=   �� 5      $ 2��6   $��1q�6*   
�������65   5       � 5!      -   5        $ Content_Area��5!      $��12q�6)    � RowDefinitions 5"      �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $��* �6$   �� 5&      $
��Auto �6$   .+�5(      �� $	��475��6   $	��180��6%   ���	#FFF7941E._�5)      � $��0,60,0,0q�6    5*      �� 5+      $
��Auto �6,   �� 5,      $
��Auto �6,   �� 5-      $��* �6,   .+�5/      �� 
�������$��Center=�67   $ ��When Only the Best Will Do��6T   $��36(�50   $   $��Segoe UI Light)�62   �� 51      $��1��6$   $	 0.5Q�61   
�������$��Center=�6R   $��DevExpress WPF Controls��6o   $��20(�52   $   $��Segoe UI Light)�62   53      54      �� 55      -   5         ��Name$
 Info��55      $��	60,35,0,0q�6*   $
��Wrap=�6=   $��Starting...��6Q   $��1��6d   ,  &DevExpress.Xpf.Editors.ProgressBarEdit  56      -   5          Name$ progressBar��56   &   $��20��6;   $	��355��6G   $��2��6S   $��0,2,0,0q�6`   $	��Top=�6q   
�������   StyleSettings 57      <	  6DevExpress.Xpf.Editors.ProgressBarMarqueeStyleSettings	  58      5:      ]� 5;      -   5         ]�Name$ Footer��5;      $��3��6,   $��55��69   $��-11,0,-11,-11q�6E   ���	#FFEEEEEE.+�5<      �� -	   5        $ Footer_Text��5<       $
��Wrap=�65   $��20,0,0,0q�6I   $
��Left=�6[   $��Center=�6v   /-Copyright © 1998-2014 Developer Express Inc.�� 5>      All Rights Reserved5@      �� 5A      -
   5         ��Name$ Logotype��5A      $��Right=�6.   $��Logo.png��6E   $
��None=�6W   $��Right=�6f    . $��0,0,20,0q�6�   5B      5C      5D      5E      5F      