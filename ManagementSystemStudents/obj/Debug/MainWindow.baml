   M S B A M L   `   `   `  ����S  OManagementSystemStudents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ManagementSystemStudentsManagementSystemStudents  5   	   )    #ManagementSystemStudents.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ManagementSystemStudents     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��450��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��3* �6   �� 5      $
��0.5* �6   .+�5   
   �� $��Students List��6   $��Center=�6*   $��Center=�6G   $��20(�6b   6r   �� 5   
   $��1��6   $��Choose group��6$   $��Center=�68   $��Center=�6U   $��15(�6p   6�   �� 5   
   $��1��6   $��1��6%   .+�5      �� $��
20,0,20,20q�6    �� SelectedIndex$ 0��6+   .@�5      �� .��6   NoSelectedGroup60   5      �� 5      $��15(�6   $��20,0,0,0q�6&   	Addition:6C   �� 5      -   5        $��Add Student��5      $��35��6+   $��
20,0,20,20q�6:   6N   �� 5       -   5        $��	Add Group��5       $��35��6)   $��
20,0,20,20q�68   6L   �� 5!      -   5        $��Add Lecture��5!      $��35��6+   $��
20,0,20,20q�6:   6N   5#      �� 5%   
   $��0��6   $��1��6"   $��20,0,0,0q�6/   ��5&      �� 5'      .R�5(      �� $��5q�6!   .+�5)      �� $��18(�6$   ��62   ��  �� Path$ Title��� 5*      ��6$   �� $ Company��� 5+      ��6$   �� $ Price�5,      5-      5/      � 53   
   $��1��6   $��1��6    54      �� 55      $��1* �6    6.   �� 56      $
��0.2* �6    60    59      �� 5:      �� 5;      5C      5E      5F      