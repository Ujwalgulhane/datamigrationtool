   M S B A M L   `   `   `  ����J  Fmydatamigration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:mydatamigration.migrationtaskmydatamigration.migrationtask  5   	   7    1mydatamigration.migrationtask.MigrationTaskWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:mydatamigration.migrationtask     5   	     � Title$  Migrate Data��5      $	��800��6   $
��1200��6*    � WindowStartupLocation$ CenterScreen=�67    � WindowState$ 	Maximized=�6\   ���#f5f5f5 � Icon$ ../resources/elablogo.png��6�   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��289.6 �6   �� 5      .+�5   
   �� ���#2c3e50$��0��6*   $��0,0,9.6,0.4q�6:   .+�5      �� $��Data Migration Tool��6   $��18(�63   $
��Bold"�6A   
�������$��20q�6f   $��Center=�6r   $��Center=�6�   �� 5      $��10q�6   �� 5      -   5        $��	Dashboard��5      $
��10,5q�6)   $��40��67   ���#34495e
�������   SidebarButtonStyle#��[  6k   6�   �� 5      -   5        $��Migrate Data��5      $
��10,5q�6,   $��40��6:   ���#34495e
�������#��[  6n   6�   �� 5      -   5        $��Migration Tasks��5      $
��10,5q�6/   $��40��6=   ���#3498db
�������#��[  6q   6�   �� 5      $��Reports��6   $
��10,5q�6'   $��40��65   ���#34495e
�������#��[  6i   �� 5      $��Settings��6   $
��10,5q�6(   $��40��66   ���#34495e
�������#��[  6j   �� 5      -   5        $��Logout��5      $
��10,5q�6&   $��40��64   ���#e74c3c
�������#��[  6h   6�   5      � 5"   
   $��1��6   $��10.4,10,9.6,10.4q�6    � RowDefinitions 5#      �� 5$      $
��Auto �6    �� 5&      $��* �6    .+�5+      �� $��Migration Tasks��6   $��24(�6/   $
��Bold"�6=   ���#34495e$��10q�6d   � 5.      $��1��6   $��10q�6     5/      �� 50      $
��Auto �6$   �� 52      $��* �6$   .+�57      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�66   .+�58      �� $��Filter by Status:��6    $��Center=�69   $��0,0,10,0q�6T   �� 59      $	��150��6   $��0,0,10,0q�6+   .@�5:      �� $	��All��6'    �� 
IsSelected . 65   �� 5;      $��	Completed��6'   �� 5<      $��In Progress��6'   �� 5=      $��Failed��6'   5>      �� 5?      $	��200��6   $��0,0,10,0q�6+   �� 5@      $��Search��6   ���#3498db
�������$��80��6V   5A      &   System.Windows.Controls.DataGrid  5D      $��1��6      AutoGenerateColumns .  	   HeadersVisibility$	 Column=�6D   $
��0,10q�6_   
   Columns
 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Header$ Task ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *TaskID+6Y     5G      $ 	Task Name��6-   $ * 6]    6@   �� *
TaskName+6]     5H      $ Status��6-   $ * 6X    6=   �� *Status+6X     5I      $ 
Start Time��6-   $ * 6_    6A   �� *	StartTime+6_     5J      $ End Time��6-   $ * 6[    6?   �� *	EndTime+6[   4  .System.Windows.Controls.DataGridTemplateColumn  5K      $ Actions��61   $ * 6B      CellTemplate 5L      �� 5M   "   .R�5N   &   �� $ 
Horizontal=�61   $��Center=�6J   .+�5O   *   �� $
��View��61   $	��5,0q�6@   ���#2ecc71
�������$��60��6u   �� 5P   *   $��Retry��61   $	��5,0q�6A   ���#f1c40f
�������$��60��6v   �� 5Q   *   $��Delete��61   $	��5,0q�6B   ���#e74c3c
�������$��60��6w   5R   '   5S   #   5U      5W      5X      5Y      5Z      5[      