   M S B A M L   `   `   `  ����  mydatamigration     mydatamigration.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$!  Data Migration Tool - Login��5   	   $	��549��5   	   $	��600��6    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Icon$ resources/elablogo.png��6   .��5      � .+�5   
   � ���#f4f4f4 � ColumnDefinitions 5      �� 5      $	��600 �6#   .+�5      �� ���	#FF092E4B$	��100��6,   $	��Top=�69   $��
0,0,-0.4,0q�6Q   ._�5      �� $��Data Migration Tool��6   
�������$��24(�5      $
��Bold"�5      $��Center=�5       �� TextAlignment$ Center=�5      $��Center=�5      $��182,26,176.6,42q�65   $	��235��6N   5      �� 5      
������� �� CornerRadius$ 10��6(   $��Center=�5      $��Center=�6.   $	��400��5       �� Padding$ 20q�6   $��97,71,96.6,80.8q�6*   $	��369��6C   ._�5      �� .+�5!      �� $��Login��6    $��20(�5"      $
��Bold"�6*   ���#333$��Center=�5$      $��0,0,0,20q�5%      �� 5(      $
��0,10q�6!   .+�5)      �� $��Username��6$   $��14(�64   ���#666$��25��6T   �� 5*      -   5        $��UsernameTextBox��5*   "   $��35��5+      $��14(�6*   $��5q�5,      5-      �� 50      $
��0,10q�6!   .+�51      �� $��Password��6$   $��14(�64   ���#666$��25��6T   G� 52      -   5        $��PasswordBox��52   &   $��35��53   "   $��14(�6.   $��5q�54   "   55      �� 58      -   5        $��LoginButton��59      $��Login��58      $	��150��5:      $��40��6%   ���	#FF092E4B
�������$��16(�5=      $
��Bold"�6'   $��Center=�5>      $
��0,20q�5?       � Cursor$
 Hand��5@   3   �� 5C      $!��© 2024 Data Migration Tool��6    $��12(�5D      ���#999$��Center=�5E      $��0,20,0,0q�5F      5G      5H      5I      � 5J   
   -   5        	 �Name$	 PreloaderOverlay��5J      ���	#80000000$y�	Collapsed=�6@   .+�5K   
   �� $��Center=�6   $��Center=�60   .+�5M      � 
 � IsIndeterminate
 . $	��100��61   $��10��6=   ���#3498db6I   �� 5N      $ ��Logging in, please wait...��6   
�������$��0,20,0,0q�6M   $��14(�6_   $��Center=�6m   5O      5P      5Q      5R      