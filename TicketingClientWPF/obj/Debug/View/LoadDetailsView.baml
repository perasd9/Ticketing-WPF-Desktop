   M S B A M L   `   `   `  ����M  ITicketingClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TicketingClientWPF.ViewTicketingClientWPF.View  5   	   K*clr-namespace:TicketingClientWPF.ViewModelTicketingClientWPF.ViewModel  5   	   -    'TicketingClientWPF.View.LoadDetailsView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TicketingClientWPF.View     5   	   @	viewModel*clr-namespace:TicketingClientWPF.ViewModel     5   	   -   5          � Title$  LoadDetailsView��5	   	   $	��710��6!   $	��800��6.    � 
ResizeMode$ NoResize=�5
   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	   .��5      ��  �� CornerRadius$ 10��6   $��10q�6   ���#e0e4e2��5   
   �� 5       �� ImageSource$ Images/background.jpg��6   ._�5   
   �� $��0q�6    J� Opacity$	 0.9Q�6%   $ 20��63   ._�5      � ���#111111 � RowDefinitions 5      �� 5      $��45 �6$   �� 5      $��* �6$   	 � ColumnDefinitions	 5      �� 5      $	��100 �6'   �� 5       $��* �6'   �� 5!      $	��100 �6'   .+�5%      �� $��0��6   $��3��6*   $��45��6>   .+�5'      � 	 5(      �� 5)      �� 5*      $��50 �6/   �� 5+      $��50 �6/   .+�5.      �� -   5        $��1��5.   !   $��40��61   $��
0, 0, 0, 5q�6=   $��
0, 0, 0, 0q�6R   
�������.��50      �� $��Images/restore-down.png��6$   $��26��6E   ��51      �� 52   "   
 �� 
TargetType"
 ��6(   .�53   &   ��  �� Property �@�Cursor �� Value$
 Hand��6?   �� 54   &    � �� �#001fff6C    �� Triggers 55   &   P� 56   *    P� Property �@J�IsMouseOver P� Value . . �57   .   ��  � �� �#007fff6K   58   +   5:   #   ��5<      �� 5=   "    �� 
TargetType" ��62   .R�5>   &   �� $ 10��6-   #��z��6?   ._�5?   *   �� $��Center=�6;   $��Center=�5@   1   5A   '   5B   #   5D      �� 5F      -   5        $��5��5F   !   $��40��61   $��
0, 0, 0, 5q�6=   $��
0, 0, 0, 0q�6R   $��X��5G      
�������$��20(�5I   !   ��5J      �� 5K   "   "
 ��6(   .�5L   &   ��  �@�Cursor$
 Hand��6?   �� 5M   &    � �� �#E334376C    5N   &   P� 5O   *    �@J�IsMouseOver . . �5P   .   ��  � �� �#FF7F7F6K   5Q   +   5S   #   ��5U      �� 5V   "   " ��62   .R�5W   &   �� $ 10��6-   #��z��6?   ._�5X   *   �� $��Center=�6;   $��Center=�5Y      5Z   '   5[   #   5]      5^      5_      � 5c      -   5         �Name$ mainGrid��5c      $��1��6)   $��1��69   $��Center=�6F   $��Center=�6a   5e      5g      5h      5i      5j      