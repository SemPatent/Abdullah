   M S B A M L   `   `   `  ����  Practicals_2014)    #Practicals_2014.Pages.PatentsFromDB   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFirstFloor.ModernUI, Version=1.0.5.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      $��481.133��5	      $	��886��6   .��5
      �    ContentRoot#��[  6   .+�5   
   �� $��
0,-10,0,10q�6   .��5      �� $	��200��6   .+�5      �� $=��7Process English and Russian Patents From Local Database��6     Heading2#��[ 6[   $��18(�6}   �� 5      -   5          ��Name$  
DBLocation��5      $��Database Location��6-   $
��Left=�6I   $��
0,15,20,20q�6d   $	��150��6x   $��30��6�   6�   �� 5      -   5        $  SaveLocation��5      $��Save Location��6/   $
��Left=�6G   $��
0,30,20,20q�6b   $	��150��6v   $��30��6�   6�   �� 5      -   5        $  	GetPatent��5      $��Get Extracts��6,   $
��Left=�6C   $��0,140,20,20q�6^   $	��150��6s   $��30��6    J� RenderTransformOrigin$ 0.5,0.5&�6�   5      5      �� 5   
   -   5         ��Name$ saveLocation��5      $��label��6&   $��30��66   $	��854��6B   $
��Left=�6N   $��0,146,0,261.333q�6i   $ 0.499,0.467&�6�   �� 5   
   -   5        $ 
dbLocation��5      $ ��Patents Database Location:��6$   $��30��6I   $	��854��6U   $
��Left=�6a   $��0,63,0,344.333q�6|   $ 0.499,0.467&�6�   � 5   
   -   5         �Name$ pbStatus��5      $
��Left=�6(   $��28��6C   $��	0,377,0,0q�6O   $	��Top=�6b   $	��844��6z    � Minimum$ 0Q�6�    � Maximum$	 100Q�6�    � BindingGroup 5      &   System.Windows.Data.BindingGroup  5      5      �� 5   
   $��Center=�6]   $	��Top=�6z   $��	0,377,0,0q�6�   ��6   ��  �� ElementName$ pbStatus�� �� Path$ Value�	 �� StringFormat$	 {0:0}%��6�   �� 5   
   -   5        $ StatusLabel��5      $
��Left=�6%   $��	0,352,0,0q�6@   $	��Top=�6S   $	��844��6k   $��20��6w   .��5      �� $��Ready��6   5       5!      5"      