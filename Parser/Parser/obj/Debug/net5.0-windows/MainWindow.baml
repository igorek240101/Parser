   M S B A M L   `   `   `  ����A  =Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ParserParser  5   	       Parser.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Parser     5   	     � Title$  
MainWindow��5   	   $	��750��6   $
��1200��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   .+�5   
   �� -   5        $��	workSpace��5      $��1��6&   $��1��63   .@�5      �� ��5      �� 5      -   5        .��5   L   Читать данные5      ��5      � 5       5      �� 5      $��1* �6,   �� 5      $	��150 �6,   .+�5      R� -   5         R�Name$
 view��5   0   $��0��6#   $
��Left=�6>   $	��Top=�6Y   $��50, 50, 50, 50q�6q   
������ 
������ 5       �� 5!      -   5         ��Name$
 Left��5!   .   $��1��6!   $��50��6<   $��50��6G   $��50, 0, 0, 50q�6S   $
��Left=�6i   $��Bottom=�6�   6�   �� 5"      -   5        $ Right��5"   .   $��1��6!   $��50��6=   $��50��6H   $��0, 0, 50, 50q�6T   $��Right=�6j   $��Bottom=�6�   6�   5#      5%      �� 5&      ��5'      �� 5(      -   5        .��5(   ?   Обновить данные5*      .��5,      �� .��5-      �� .+�5.      �� -   5         ��Name$ Status��5.       $
��Left=�60   $	��Top=�6K   $��50,50,50,50q�6c   5/      R� 50      -   5        $ Result��50   #   $
��Left=�63   $	��Top=�6N   $��
50,0,50,50q�6f   
������ 
������ 51      52      53      54      55      � 56   
   -	   5         �Name$ progressBar��56   0    � Minimum$ 0Q�6    � Maximum$	 100Q�6"   $
��Left=�6E   $��14��6`   $��
245,10,0,0q�6l   $	��Top=�6�   $	��930��6�   57      58      