   M S B A M L   `   `   `  ����F  BMinesweeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MinesweeperMinesweeper  5   	       Minesweeper.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Minesweeper     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5	      � -   5        $
��main��5	       � ShowGridLines$ False��6   .+�5   
   �� -   5         ��Name$ TextBoxDimensions��5   @   $
��Left=�6   $��	25,10,0,0q�6-    �� TextWrapping$
 Wrap=�6[   $	��Top=�6o   $	��120��6�   �� 5   
   -   5        $ TextBoxMines��5   @   $
��Left=�6   $��	25,33,0,0q�6-   $
 Wrap=�6X   $	��Top=�6l   $	��120��6�   �� 5   
   -   5         ��Name$ ApplyButton��5      $��Apply��6(   $��64��68   $��32��6C   $��-175,-17,511,385q�6Q   �� 5   
   -   5         ��Name$
 time��5      $
��time��6    $��25,112,711,0q�6/   $	��Top=�6E   � 5   
   -   5         �Name$
 grid��5      $ False��6   $	��400��63   $	��400��6@   5      5      5      